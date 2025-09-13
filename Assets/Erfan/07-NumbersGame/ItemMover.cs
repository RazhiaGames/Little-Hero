using System;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    private static ItemMover currentlyDragging = null;

    private Vector3 offset;
    private Camera mainCamera;
    public LayerMask hitMask;
    public bool isInBox;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleInput();

        // Move the selected object if dragging
        if (currentlyDragging == this)
        {
            Vector3 newPos = GetMouseWorldPosition() + offset;
            transform.parent.position = new Vector3(newPos.x, transform.parent.position.y, newPos.z);
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, hitMask))
            {
                var digit = hit.collider.GetComponent<ItemMover>();
                if (digit == this)
                {
                    currentlyDragging = this;
                    offset = transform.parent.position - GetMouseWorldPosition();
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && currentlyDragging == this)
        {
            currentlyDragging = null;
            if (isInBox)
            {
                enabled = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NumbersContainer>(out var mamad))
        {
            isInBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NumbersContainer>(out var mamad))
        {
            isInBox = false;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}