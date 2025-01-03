using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm : MonoBehaviour
{

    // left controller for getting direction
    public Transform leftArm;
    public Transform body;
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
        orientation = leftArm.rotation;
        // To curb y rotation
        
        //Quaternion bodyRotation = body.rotation;
        //float rotationDifference =Mathf.DeltaAngle(bodyRotation.y, orientation.y);

        //orientation.y = Mathf.Clamp(rotationDifference, -verticalRotationLimit, verticalRotationLimit);
        // copy rotation with my curbed values
        transform.rotation = orientation;
    }
}
