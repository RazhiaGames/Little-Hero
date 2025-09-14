using System.Collections;
using System.Collections.Generic;
using RTLTMPro;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 1000f;
    public Animator Anime;
    private Vector3 velocity;
    private bool isGrounded;
    private float yRotation = 0f;
    public CharacterController controller;
    public Transform playerBody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
   
    void Update()
    {
        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        //yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        playerBody.Rotate(Vector3.right * mouseY * 100);
        transform.localRotation = Quaternion.Euler(0f,yRotation, 0f);

        // Handle movement
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Resetting velocity when grounded
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * speed * Time.deltaTime);
        if ((moveX!=0)||(moveZ!=0))
        {
            Anime.SetBool("MoveFWD",true);
        }
        else
        {
            Anime.SetBool("MoveFWD",false);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
