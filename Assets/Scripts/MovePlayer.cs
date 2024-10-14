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

    Vector3 moveDirection;
    Rigidbody rb;

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
        Vector2 verticalInput = move.action.ReadValue<Vector2>();
        moveDirection = transform.forward * -(verticalInput.x);
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Acceleration);
        //transform.localEulerAngles = Vector3.zero;


        
    }

    private void OnDisable()
    {
        move.action.Disable();
    }
}
