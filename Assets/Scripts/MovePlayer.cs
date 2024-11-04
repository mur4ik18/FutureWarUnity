using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    // For getting input from controllers
    public InputActionReference move;
    public InputActionReference rotate;

    [Header("Movement")]
    public float moveSpeed;
    public float rotationSpeed = 1f;
    public float maxSpeed = 10f;

    Vector3 moveDirection;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        move.action.Enable();
        rotate.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 verticalInput = move.action.ReadValue<Vector2>();
        
        moveDirection = transform.forward * (verticalInput.y);


        // If there arn't input, there arn't moving
        if (moveDirection.x != 0 && moveDirection.y != 0 && moveDirection.z != 0)
        {

            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            //Debug.Log(moveDirection);

            // limit speed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            
        }



        // Rotatoin
        Vector2 horizontalInput = rotate.action.ReadValue<Vector2>();

        float targetRotationY = horizontalInput.x * rotationSpeed;
        Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0) * transform.rotation;

        //Debug.Log("Rot : " + targetRotation);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnDisable()
    {
        move.action.Disable();
        rotate.action.Disable();
    }
}
