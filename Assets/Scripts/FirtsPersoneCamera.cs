using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirtsPersoneCamera : MonoBehaviour
{
    public Transform player;
    public float mSensivity = 3f;
    public float cameraVerticalRotation = 0f;
    public float verticalRotationLimit = 45f;
    public Camera cam;
    public Transform orientation;
    // block horizontal input
    public bool HBlocking = false;

    public float cameraHorizontalRotation = 0f;
    public float horizontalRotationLimit = 50f;

    public float delay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        // hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // collect input
        float inpX = Input.GetAxis("Mouse X") * mSensivity;
        float inpY = Input.GetAxis("Mouse Y") * mSensivity;

        float inpArrowsX = Input.GetAxis("") * mSensivity;

        // rotate camera verical
        cameraVerticalRotation -= inpY;
        //cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -verticalRotationLimit, verticalRotationLimit);
    
        // rotate camera horizontal
        cameraHorizontalRotation += inpX;
        // Limit camera movement
        //cameraHorizontalRotation = Mathf.Clamp(cameraHorizontalRotation, -horizontalRotationLimit, horizontalRotationLimit);

        // Apply horizontal rotation to the player
        // move player
        //transform.localEulerAngles = Vector3.left * cameraVerticalRotation;
        player.localEulerAngles = new Vector3(cameraVerticalRotation, player.localEulerAngles.y, player.localEulerAngles.z);




        if (!HBlocking)
        {
            //transform.localEulerAngles = Vector3.up * cameraHorizontalRotation;
            player.localEulerAngles = new Vector3(player.localEulerAngles.x, cameraHorizontalRotation, player.localEulerAngles.z);
        }
        else
        {
        }
    }
}