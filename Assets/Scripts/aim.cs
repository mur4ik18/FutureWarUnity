using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class aim : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    public InputActionReference trigger;

    private void Update()
    {
    }


    void OnEnable()
    {
        // Register event listeners for trigger activation
        trigger.action.started += OnTriggerStarted;
        trigger.action.performed += OnTriggerPerformed;
        trigger.action.canceled += OnTriggerCanceled;
    }

    void OnDisable()
    {
        // Unregister event listeners
        trigger.action.started -= OnTriggerStarted;
        trigger.action.performed -= OnTriggerPerformed;
        trigger.action.canceled -= OnTriggerCanceled;
    }

    private void OnTriggerStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger started");
        var bullet = Instantiate(bulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.forward * bulletSpeed;
    }

    private void OnTriggerPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger performed");
    }

    private void OnTriggerCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger canceled");
    }
}
