using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SpiderStates
{
    followOnGround,
    casting,
    idle
}
public class SpiderAI : MonoBehaviour
{
    [Header("Movement Controls")]
    //Speed that the spider moves
    public float followSpeed;
    //Distance from an object that the spider has to be to avoid it
    public float avoidRadius;
    public float distanceToSlowDown;
    public float closenessToStartIdleing;

    [Header("Behavior Controls")]
    public float avoidObjectsBias;
    public float targetBias;

    private Transform playerTransform;
    public SpiderStates currentState;

    private bool isCasting;


    [Header("Web Platform Properties")]
    public float WaitBeforeCast;
    public float castDistanceFromPlayer;

    //References
    private Rigidbody rb;
    public Transform cameraTransform;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        currentState = SpiderStates.followOnGround;
    }

    public void Update()
    {
        EnemyAction();
    }

    public KeyCode castWebKey;
    private void EnemyAction()
    {
        //A switch case is like a bunch of if statements,
        //it searches for whatever your current state is, and goes to that behavior function
        switch (currentState)
        {
            case SpiderStates.followOnGround:
                if ((playerTransform.position - transform.position).magnitude < closenessToStartIdleing) currentState = SpiderStates.idle;
                if (Input.GetKeyDown(castWebKey)) currentState = SpiderStates.casting;
                FollowingOnGround();
                break;
            case SpiderStates.casting:
                CastingBehavior();
                break;
            case SpiderStates.idle:
                if ((playerTransform.position - transform.position).magnitude >= closenessToStartIdleing) currentState = SpiderStates.followOnGround;
                if (Input.GetKeyDown(castWebKey)) currentState = SpiderStates.casting;
                break;
        }

    }

    public LayerMask avoidLayer;
    private Vector3 GetAvoidVector()
    {
        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, avoidRadius, avoidLayer);
        Vector3 avoidVector = Vector3.zero;
        for(int i = 0; i < objectsInRadius.Length; i++) 
        {
            Vector3 difference = transform.position - objectsInRadius[i].ClosestPoint(transform.position);
            avoidVector += difference.normalized / difference.magnitude;

        }
        return avoidVector;
    }
    private void FollowingOnGround()
    {
        Vector3 moveVelocity = GetAvoidVector()*avoidObjectsBias;
        moveVelocity += (playerTransform.position - transform.position).normalized*targetBias;
        
        Debug.DrawRay(transform.position, moveVelocity, Color.yellow);

        Vector3 playerDirection = playerTransform.position - transform.position;
        moveVelocity = moveVelocity.normalized;

        float speedMultiplier = followSpeed;

        float slowDownAmount = Mathf.Clamp(Vector3.Dot(moveVelocity, playerDirection.normalized), 0f, 1f);
        slowDownAmount *= 1 - Mathf.Clamp(playerDirection.magnitude, 0f, distanceToSlowDown)/distanceToSlowDown;

        speedMultiplier -= followSpeed * slowDownAmount;

        rb.linearVelocity = moveVelocity.normalized * speedMultiplier;
        transform.rotation = Quaternion.LookRotation(-moveVelocity.normalized);
    }

    private void CastingBehavior()
    {
        if (!isCasting)
        {
            isCasting = true;
            StartCoroutine(CastWeb());
        }
    }

    IEnumerator CastWeb()
    {
        yield return new WaitForSeconds(WaitBeforeCast);
        //Cast from player to wall
        Ray cameraRay = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hitInfo = new RaycastHit();
        //Physics.Raycast(cameraRay, hitInfo);

    }
}
