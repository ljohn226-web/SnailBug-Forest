using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    public RiverBehavior river; //ref to river script
    public KittyAI kitty; //ref to kitty
    public Transform playerPos;
    public bool fishWasCaught = false;


    //Fishing Inventory/visuals
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // kitty = GetComponent<KittyAI>();
      //  playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            //cast ray toward mousepos
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            //if ray hit collider river, send start to BUBBLE SPAWN
            if (Physics.Raycast(ray, out hit))
            {
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
                    FishSuccess();
                    //get collider of hit object, and destroy hit object 
                    Destroy(hit.collider.gameObject);
                    
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
                        FishSuccess();
                    }
                    else
                    {
                        fishWasCaught = false;
                        FishFail();
                    }
                    //if number is less than .7, !fishWasCaught
                    //get collider of hit object, and destroy hit object 
                    GameObject bub = hit.collider.gameObject;

                    river.RemoveBubble(bub);
                    Destroy(bub);   
                   

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

    public void FishSuccess()
    {
        Debug.Log("Kitty caught a Fish!");
        kitty.CatchFish(); //null right now FIXED
    }

    public void FishFail()
    {
        Debug.Log("Uh Oh! the fish got away!");
        kitty.MissFish(); //null right now FIXED
    }
}
