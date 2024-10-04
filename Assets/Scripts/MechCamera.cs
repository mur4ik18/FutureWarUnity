using UnityEngine;
using System.Collections;

public class MechCamera : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 200f;
    public float cameraVerticalRotation = 0f;
    public float verticalRotationLimit = 45f;

    public float cameraHorizontalRotation = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraHorizontalRotation += mouseX;

        // Rotate the player's body horizontally (yaw)
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically (pitch), and clamp to prevent flipping
        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -verticalRotationLimit, verticalRotationLimit);

        // Apply the rotation to the camera's local rotation
        transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
        // transform.localRotation = Quaternion.Euler(0f, playerBody.localRotation.y * 10f, 0f);
    }
}
