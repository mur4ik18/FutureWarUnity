using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference rotate;

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float rotationSpeed = 1f;

    Vector3 moveDirection;
    Rigidbody rb;


    [Header("Ground check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);


        Vector2 verticalInput = move.action.ReadValue<Vector2>();
        
        moveDirection = transform.forward * (verticalInput.y);

        if (moveDirection.x != 0 && moveDirection.y != 0 && moveDirection.z != 0)
        {
            Debug.Log(moveDirection);
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        //transform.localEulerAngles = Vector3.zero;

        Vector2 horizontalInput = rotate.action.ReadValue<Vector2>();

        float targetRotationY = horizontalInput.x * rotationSpeed;
        Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0) * transform.rotation;

        Debug.Log("Rot : " + targetRotation);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        //transform.Rotate(Vector3.up , horizontalInput.x * rotationSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        move.action.Disable();
        rotate.action.Disable();
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }
}
