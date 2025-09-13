using UnityEngine;

[SelectionBase]
public class DragObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    public Common.ArrangingGameItemType arrangingGameItemType;
    public Common.ArrangingGameItemType secondArrangingGameItemType = Common.ArrangingGameItemType.None;
    public bool isInBox;

    public Vector3 defaultScale;

    void Start()
    {
        mainCamera = Camera.main;
        defaultScale = transform.localScale;
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (isInBox)
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            newPosition.y = transform.position.y; // Keep original y position
            transform.position = newPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Container>(out var mamad))
        {
            isInBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Container>(out var mamad))
        {
            isInBox = false;
        }
    }
}