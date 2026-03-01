using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public Transform lizardMouth;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    public float grapplingCdTimer;

    [Header("Input for Grapple")]
    public KeyCode grapplingKey = KeyCode.RightShift;

    private bool grappling;

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

        RaycastHit hit;
        //raycast that starts from the cam, goes the max grapple distance, and checks if the terrain is grappleable
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
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

    }
    //stop and cooldown gets activated
    private void StopGrapple()
    {
        grappling = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }
}
