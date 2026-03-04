using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement variables
    public float moveSpeed;
    public Transform orient;

    float horizInput;
    float vertInput;

    Vector3 moveDirection;
    Rigidbody rb;

    public float groundDrag;
    //ground check
    public float playerHeight;

    public LayerMask whatIsGround;
    bool grounded;

    

    [Header("Grappling")]
    public bool freeze;
    public bool activeGrapple;
    private bool enableMovementOnNextTouch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }
    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        //drag 
        if (grounded && !activeGrapple)
            rb.linearDamping = 1f;
        else if (!grounded && !activeGrapple)
            rb.linearDamping = 3f;
        else
            rb.linearDamping = 0f;

        //if (freeze)
        //{
        //    rb.linearVelocity = Vector3.zero;
        //}
    }

    //fixed so player movement is not frame dependant
    private void FixedUpdate()
    {
        MovePlayer();
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
    }
    private void MyInput()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {
        if (activeGrapple) return;
        //calculate movement in the direction you are looking in
        moveDirection = orient.forward * vertInput + orient.right * horizInput;

        //add force
        rb.AddForce(moveDirection.normalized * moveSpeed * 5f, ForceMode.Force);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
        grounded = false;
    }

    //function that calculates jump velocity
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;

        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    //apply the force with a bit of delay
    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.linearVelocity = velocityToSet;
        Debug.Log(velocityToSet);
    }

    //function that makes the player jump to the targeted position
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
 
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        
        //velocity is applied after 0.f seconds
        Invoke(nameof(SetVelocity), 0.1f);
        Invoke(nameof(ResetRestrictions), 3f);
        Debug.Log(":(");
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();
            GetComponent<Grappling>().StopGrapple();
        }
   
    }
}
