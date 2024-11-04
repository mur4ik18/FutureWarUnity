using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArm : MonoBehaviour
{
    // left controller for getting direction
    public Transform rightArm;
    // limitation of Y angle 
    public float verticalRotationLimit = 0.2f;
    // To show data
    public Quaternion orientation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Here I get controller rotation
        orientation = rightArm.rotation;
        // To curb y rotation
        //orientation.y = Mathf.Clamp(orientation.y, -verticalRotationLimit, verticalRotationLimit);
        // copy rotation with my curbed values
        transform.rotation = orientation;
    }
}
