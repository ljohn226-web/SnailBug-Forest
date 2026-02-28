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
        if (grounded)
            rb.linearDamping = 0.2f;
        else
            rb.linearDamping = 0.0f;
    }

    //fixed so player movement is not frame dependant
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {
        //calculate movement in the direction you are looking in
        moveDirection = orient.forward * vertInput + orient.right * horizInput;

        //add force
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
