using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get the mouse input and apply sensitivity and time delta to ensure smooth and consistent camera movement regardless of frame rate.
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Update the camera's rotation based on the mouse input, allowing for looking around in the game world. The yRotation is updated with the horizontal mouse movement,
        // while the xRotation is updated with the vertical mouse movement.
        // The xRotation is clamped to prevent excessive vertical rotation, ensuring a more natural and comfortable camera control experience.
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the calculated rotations to the camera's transform and the orientation transform. The camera's rotation is set to the calculated xRotation and yRotation,
        // while the orientation's rotation is set to only the yRotation, allowing for independent control of the camera's vertical and horizontal rotation.
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
