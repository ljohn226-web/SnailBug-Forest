using UnityEngine;

public class KittyAI : MonoBehaviour
{
    public enum KittyStates
    {
        trapped,
        followOnGround,
        fish,
        idle
    }

    [Header("Move Controls")]
    public float followSpeed;
    public float avoidRadius;
    public float idleDistance;

    [Header("Behavior controls")]
    public float avoidObjectsBias;
    public float targetBias;

    private Transform playerTransform;
    public KittyStates currentState;

    private bool isFishing;   //true when fishing
    private bool isInRiver;   //true when senses river collider
    private bool inFishRange; //becomes true when sense river bank collider

    [Header("Fishing Properties")]
    public float fishCooldown;
    //public float fishTime;
    public KeyCode fishKey;

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
        if (collision.collider.CompareTag("Player"))
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        }
    }

    private void CatAction()
    {
        //a switch case searches for your current state,
        //and triggers behavior function based on state
        switch (currentState)
        {
            case KittyStates.trapped: 
              //if player is equal to or out of idle dist, = follow
                if ((playerTransform.position - transform.position).magnitude >= idleDistance) currentState = KittyStates.followOnGround;
                if ((playerTransform.position - transform.position).magnitude < idleDistance) currentState = KittyStates.idle;
                TrappedInTree();
                break;

            case KittyStates.followOnGround:
              //if player not moving and player is in idle range, current = idle 
                if ((playerTransform.position - transform.position).magnitude < idleDistance) currentState = KittyStates.idle;
                if (!isInRiver && inFishRange && fishCooldown <= 0 && Input.GetKeyDown(fishKey)) currentState = KittyStates.fish;
                FollowingPlayer();
                break; 

            case KittyStates.fish:
                //if fishing time is done, current = idle
                if ((playerTransform.position - transform.position).magnitude >= idleDistance) currentState = KittyStates.followOnGround;
                if ((playerTransform.position - transform.position).magnitude < idleDistance) currentState = KittyStates.idle;
                Fish();
                break;

            case KittyStates.idle:
              //if player is in fish range AND not touching river
              //AND fishCooldown = 0 AND press fishKey, current = fishing
              //if player is moving AND is out of idle range, current = follow
                if (!isInRiver && inFishRange && fishCooldown  <= 0 && Input.GetKeyDown(fishKey)) currentState = KittyStates.fish;
                if ((playerTransform.position - transform.position).magnitude >= idleDistance) currentState = KittyStates.followOnGround;
                break;
        }
    }

    //when does each behavior end? 
    void TrappedInTree() 
    {
        //cat sit and look around
        currentState = KittyStates.trapped;
    }

    void FollowingPlayer()
    {
        //follow player at follow distance
        //avoid obstacles
        currentState = KittyStates.followOnGround;
    }

    void Fish()
    {
       //cat swipe at water bubbles
       //based on timing, fishing successful or unsuccessful
       //start fish cooldown
       isFishing = true;
       currentState = KittyStates.fish;
    }

}
