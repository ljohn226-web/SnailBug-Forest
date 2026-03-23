using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Camera cam;
    public RiverBehavior river; //ref to river script
    public KittyAI kitty; //ref to kitty
    public Transform playerPos;
    public bool fishWasCaught = false;


    //Fishing Inventory/visuals
    public int fishCount = 0;
    public RawImage fish01;
    public RawImage fish02;
    public RawImage fish03;
    public RawImage fish04;
    public RawImage fish05;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // kitty = GetComponent<KittyAI>();
      //  playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(1))
        {
            //cast ray toward mousepos
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            //if ray hit collider river, send start to BUBBLE SPAWN
            if (Physics.Raycast(ray, out hit))
            {
                 Debug.Log("raycast");
                if (hit.collider.CompareTag("River"))
                {
                    Debug.Log("Hit river, begin bubble spawn");
                    //pass point of click and collider clicked to coroutine 
                    //is it chill if im passing the river collider to the river 
                    river.StartCoroutineExternal(hit.point, hit.collider);
                }

                //if ray hits bubble collider, call fish chance functions
                else if (hit.collider.CompareTag("BigBubble"))
                {
                    Debug.Log("Hit big bubble!");
                    fishWasCaught = true;   
                    FishSuccess(hit.collider.transform);
                    //get collider of hit object, and destroy hit object                     
                }

                //if ray hits bubble collider, call fish chance functions
                else if (hit.collider.CompareTag("SmallBubble"))
                {
                    Debug.Log("Hit small bubble!");

                    //generate random number to see if fishing is successful with 70% fail, 30% succeed bias
                    float fishProbability = Random.value;

                    //if number is .7 0r higher fishWasCaught = true
                    if (fishProbability > 0.8f)
                    {
                        fishWasCaught = true;
                        FishSuccess(hit.collider.transform);
                    }
                    else
                    {
                        fishWasCaught = false;
                        FishFail(hit.collider.transform);
                    }
                    //if number is less than .7, !fishWasCaught
                    //get collider of hit object, and destroy hit object 
                    GameObject bub = hit.collider.gameObject;
                }
            }

           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("RiverBank"))
        {
          
            Debug.Log("Collided with riverbank");

        }
    }

    public void FishSuccess(Transform bubbleTransform)
    {
        Debug.Log("Kitty caught a Fish!");
        fishCount++;
        FishyToggle();
        kitty.CatchFish(bubbleTransform); 
    }

    public void FishFail(Transform bubbleTransform)
    {
        Debug.Log("Uh Oh! the fish got away!");
        kitty.MissFish(bubbleTransform);
    }

    public void FishyToggle()
    {
        if (fishCount == 1 && fish01 != null)
            fish01.gameObject.SetActive(true);
        
        if (fishCount == 2 && fish02 != null)
            fish02.gameObject.SetActive(true);
        
        if (fishCount == 3 && fish03 != null)
            fish03.gameObject.SetActive(true);
        
        if (fishCount == 4 && fish04 != null)
             fish04.gameObject.SetActive(true);
        
        if(fishCount == 5 && fish05 != null)
            fish05.gameObject.SetActive(true);

    }
}
