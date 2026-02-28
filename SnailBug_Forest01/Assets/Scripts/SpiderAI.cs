using UnityEngine;
using UnityEngine.InputSystem;

public enum SpiderStates
{
    followOnGround,
    casting
}
public class SpiderAI : MonoBehaviour
{
    [Header("Movement Controls")]
    //Speed that the spider moves
    public float followSpeed;
    //Distance from an object that the spider has to be to avoid it
    public float avoidRadius;

    [Header("Behavior Controls")]
    public float avoidObjectsBias;
    public float targetBias;

    private Transform playerTransform;
    public SpiderStates currentState;
    


    //References
    private Rigidbody rb;
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

    private void EnemyAction()
    {
        //A switch case is like a bunch of if statements,
        //it searches for whatever your current state is, and goes to that behavior function
        switch (currentState)
        {
            case SpiderStates.followOnGround:
                FollowingOnGround();
                break;
            case SpiderStates.casting:
                CastingBehavior();
                break;
        }

    }

    private Vector3 GetAvoidVector()
    {
        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, avoidRadius);
        Vector3 avoidVector = Vector3.zero;
        for(int i = 0; i < objectsInRadius.Length; i++) 
        {
            Vector3 difference = transform.position - objectsInRadius[i].ClosestPoint(transform.position);
            difference = Vector3.SqrMagnitude(difference) * difference.normalized;
            avoidVector += new Vector3(1/difference.x, 1/difference.y, 1/difference.z);
        }
        return avoidVector;
    }

    private void FollowingOnGround()
    {
        Vector3 moveVelocity = GetAvoidVector()*avoidObjectsBias;
        moveVelocity += Vector3.zero;
        rb.linearVelocity = moveVelocity;
    }

    private void CastingBehavior()
    {

    }
}
