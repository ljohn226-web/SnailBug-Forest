using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public Transform lizardMouth;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    public float grapplingCdTimer;

    [Header("Input for Grapple")]
    public KeyCode grapplingKey = KeyCode.RightShift;

    [SerializeField]
    private bool grappling;

    public AudioClip blep;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grapplingKey)) StartGrapple();

        if(grapplingCdTimer > 0) grapplingCdTimer -= Time.deltaTime;
    }

    //late update is also called every frame, but after update runs
    private void LateUpdate()
    {
        if (grappling) lr.SetPosition(0, lizardMouth.position);
    }
    //shoot tongue out
    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        pm.freeze = true;

        GetComponent<AudioSource>().clip = blep;
        GetComponent<AudioSource>().Play();

        RaycastHit hit;
        //raycast that starts from the cam, goes the max grapple distance, and checks if the terrain is grappleable
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, pm.whatIsGround))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }
        lr.enabled = true;
        lr.SetPosition(1, grapplePoint); 
    }
    //after a delay, pull the player towards the thing
    private void ExecuteGrapple()
    {
        
        pm.freeze = false;

        //find the lowest point
        //calculating jump velocity doesn't find the lowest point for the player to move along (aka where the grappling line is)
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }
    //stop and cooldown gets activated
    public void StopGrapple()
    {
        Debug.Log("Stopped Grapple");
        pm.freeze = false;

        grappling = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }

}
