using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KittyAI : MonoBehaviour
{
    public enum KittyStates
    {
        trapped,        //want cat to sit
        followOnGround,
        fish,
        idle        //want cat to sit
    }

    [Header("Move Controls")]
    public float followSpeed;
    public float avoidRadius; 
    public float slowToIdleDist;
    public float idleDistance;

    [Header("Behavior controls")]
    public float avoidObjectsBias;
    public float targetBias;

    private Transform targetTransform;
    public KittyStates currentState;

    public bool isFishing;   //true when fishing
    private bool isInRiver;   //true when senses river collider
    private bool inFishRange = false; //becomes true when sense river bank collider

    [Header("Fishing Properties")]
    public float fishCooldown = 5f;
    public RiverBehavior RiverBehavior;
    //public float fishTime;

    public KeyCode fishKey = KeyCode.F;
    //References
    private Rigidbody rb;
    public Transform cameraTransform;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentState = KittyStates.trapped;
    }

    // Update is called once per frame
    void Update()
    {
        CatAction();

    }
    private void OnCollisionEnter(Collision collision)
    {
        //if player hasString and collides with cat, current = follow
        if (collision.collider.CompareTag("Player") && !isFishing)
        {
                targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
            rb.useGravity = true;

        }else if(isFishing && collision.transform == targetTransform)
        {
            isFishing = false;
            avoidRadius = 1;
            Destroy(targetTransform);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("RiverBank"))
        {
            inFishRange = false;
        }
    }
    private void CatAction()
    {
        //a switch case searches for your current state,
        //and triggers behavior function based on state
        if (!isFishing)
        {
            switch (currentState)
            {
                case KittyStates.trapped:
                    //if player is equal to or out of idle dist, = follow
                    if (targetTransform != null)
                    {
                        if ((targetTransform.position - transform.position).magnitude >= idleDistance) currentState = KittyStates.followOnGround;
                        if ((targetTransform.position - transform.position).magnitude < idleDistance) currentState = KittyStates.idle;
                    }

                    TrappedInTree();
                    break;

                case KittyStates.followOnGround:
                    //if player not moving and player is in idle range, current = idle 
                    if ((targetTransform.position - transform.position).magnitude < idleDistance) currentState = KittyStates.idle;
                    if (inFishRange && fishCooldown <= 0 && Input.GetKeyDown(fishKey)) currentState = KittyStates.fish;
                    FollowingPlayer();
                    break;

                case KittyStates.fish:
                    //if fishing time is done, current = idle
                    if ((targetTransform.position - transform.position).magnitude >= idleDistance) currentState = KittyStates.followOnGround;
                    //if ((playerTransform.position - transform.position).magnitude < idleDistance) currentState = KittyStates.idle;
                    if (inFishRange && fishCooldown > 0 && inFishRange) currentState = KittyStates.idle;
                    Fish();
                    break;

                case KittyStates.idle:
                    //if player is in fish range AND not touching river
                    //AND fishCooldown = 0 AND press fishKey, current = fishing
                    //if player is moving AND is out of idle range, current = follow
                    if (inFishRange && fishCooldown <= 0 && Input.GetKeyDown(fishKey)) currentState = KittyStates.fish;
                    if ((targetTransform.position - transform.position).magnitude >= idleDistance) currentState = KittyStates.followOnGround;
                    break;
            }
        }
        else
        {
            FollowingPlayer();
        }
        
    }

    /* FISHING LOGIC:
River spawns bubbles
Player clicks river to start fishing
Bubble appears
Player clicks bubble
Kitty runs to bubble and swipes
If kitty reaches bubble in time, catch fish
If notfish escapes
    */

    //when does each behavior end? 
    void TrappedInTree() 
    {
        //cat sit and look around
       
    }
    public LayerMask avoidLayer;

    private Vector3 GetAvoidVector()
    {
        //calculating avoid mechanic
        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, avoidRadius, avoidLayer);
        Vector3 avoidVector = Vector3.zero;
        for (int i = 0; i < objectsInRadius.Length; i++)
        {
            Vector3 difference = transform.position - objectsInRadius[i].ClosestPoint(transform.position);
            avoidVector += difference.normalized / difference.magnitude;

        }
        return avoidVector;
    }
    void FollowingPlayer()
    {
        
        if (targetTransform == null)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
            isFishing = false;
        }

        //avoid obstacles      
        Vector3 moveVelocity = GetAvoidVector() * avoidObjectsBias;
        moveVelocity += (targetTransform.position - transform.position).normalized * targetBias;
            

        //follow player at follow distance
        Vector3 playerDirection = targetTransform.position - transform.position;
        moveVelocity = moveVelocity.normalized;
        float speedMultiplier = followSpeed;
       
        //make cat slow into idle

        if (!isFishing)
        {
            float slowDownAmount = Mathf.Clamp(Vector3.Dot(moveVelocity, playerDirection.normalized), 0f, 1f);
            slowDownAmount *= 1 - Mathf.Clamp(playerDirection.magnitude, 0f, slowToIdleDist) / slowToIdleDist;
            speedMultiplier -= followSpeed * slowDownAmount;
        }
        

        rb.linearVelocity = moveVelocity.normalized * speedMultiplier;
        transform.rotation = Quaternion.LookRotation(moveVelocity.normalized);
    }

   
    void Fish()
    {
       //cat swipe at water bubbles
       //based on timing, fishing successful or unsuccessful
       //start fish cooldown

        if (!isFishing) //if is not fishing, start fishing 
        {
            fishCooldown = 5;
            isFishing = true;        
            
        }

        

        if (isFishing)
        {
            
        }
       //currentState = KittyStates.fish;
    }

    public void CatchFish(Transform target)
    {
        Debug.Log("Kitty caught a fish!");
        
        // currentState = KittyStates.fish;
        rb.useGravity = false;
        isFishing = true;
        avoidRadius = 0;
        SetTarget(target);
        // play catch animation here
    
    }

    public void MissFish(Transform target)
    {
        Debug.Log("Fish escaped!");

        rb.useGravity = false;
        isFishing = true;
        SetTarget(target);
        avoidRadius = 0;
        // play fail animation
    }

    public void SetTarget(Transform newTransform)
    {
        targetTransform = newTransform;
        Debug.Log(newTransform.name);
    }
}
