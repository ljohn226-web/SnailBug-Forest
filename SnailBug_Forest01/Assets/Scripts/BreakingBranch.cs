using UnityEngine;

public class BreakingBranch : MonoBehaviour
{
    [SerializeField]
    private GameObject brokenBranch;

    public float breakDelay = 1f;
    public float breakTimer;
    private bool playerOnBranch = false;
    private bool hasBroken = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        breakTimer = breakDelay;
    }

     //ADD SOUND BEFORE TREE BREAKS TO INDICATE


    // Update is called once per frame
    void Update()
    {
        //if branch collider sense player collider 
        if (playerOnBranch && !hasBroken)
        {
            breakTimer -= Time.deltaTime;
            if (breakTimer <= 0f)
            {
                {
                    BreakBranch();
                }

            }
        }
    }

    void BreakBranch()
    {
        hasBroken = true;   
        // after x seconds
        // branch destroy and broken branch w/ gravity instantiates
        Instantiate(brokenBranch, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(playerOnBranch);
            playerOnBranch = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnBranch = false;
            breakTimer = breakDelay;
        }
    }
}
