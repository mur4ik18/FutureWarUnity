using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public InputActionReference move;

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

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
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);


        Vector2 verticalInput = move.action.ReadValue<Vector2>();
        moveDirection = transform.forward * -(verticalInput.x);
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //transform.localEulerAngles = Vector3.zero;

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void OnDisable()
    {
        move.action.Disable();
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
