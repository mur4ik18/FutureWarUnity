using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float life = 3;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Idestroy " +  collision.gameObject.name);
        Destroy(gameObject);
    }
}
