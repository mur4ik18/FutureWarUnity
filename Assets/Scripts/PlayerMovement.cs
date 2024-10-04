using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    public Transform orientation;
    public Transform Head;

    public Transform cam;

    FirtsPersoneCamera camera;

    public float horizontalInput;
    public float verticalInput;

    public float rotationThreshold = 45f;

    Vector3 moveDirection;
    Rigidbody rb;

    public RectTransform LLegDirection, RLegDirection, direction;


    [Header("Ground check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        camera = (FirtsPersoneCamera)cam.GetComponent(typeof(FirtsPersoneCamera));
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }


    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // direction calculs
        moveDirection = orientation.forward * verticalInput;
        // Add Force
        // se bouger
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        
        // rotate player legs
        // Rotate head with if player legs was rotate more then 45o

        orientation.Rotate(Vector3.up * (horizontalInput/5));
        float legsYRotation = orientation.localEulerAngles.y;

        // Sprite rotation for legs
        LLegDirection.localRotation = Quaternion.Euler(0,0, -(legsYRotation-60f));
        RLegDirection.localRotation = Quaternion.Euler(0, 0, -(legsYRotation+60f));
        
        float headYRotation = Head.localEulerAngles.y;

        direction.localRotation = Quaternion.Euler(0, 0, -headYRotation);

        float rotationDifference = Mathf.Abs(Mathf.DeltaAngle(legsYRotation, headYRotation));
        // la rotation de la tete est plus grande que 60 degres
        if (rotationDifference >= 60)
        {
            //camera.HBlocking = true;
            float newHeadYRotation = Mathf.LerpAngle(headYRotation, legsYRotation, Time.deltaTime*5f);
            Head.localEulerAngles = new Vector3(Head.localEulerAngles.x, newHeadYRotation, Head.localEulerAngles.z);
            //cam.localEulerAngles = new Vector3(cam.localEulerAngles.x, newHeadYRotation, cam.localEulerAngles.z);
        }
        else
        {
            //camera.HBlocking = false;
        }

        //HeadMove();

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
