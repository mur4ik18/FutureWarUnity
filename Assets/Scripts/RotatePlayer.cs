using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlayer : MonoBehaviour
{
    public InputActionReference rotate;

    // Start is called before the first frame update
    void Start()
    {
        rotate.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 horizontalInput = rotate.action.ReadValue<Vector2>();
        transform.Rotate(Vector3.up * (horizontalInput.x));
    }

    private void OnDisable()
    {
        rotate.action.Disable();
    }
}
