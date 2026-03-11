using System.Collections;
using UnityEngine;

public class RiverBehavior : MonoBehaviour
{

    //bubble vars
    public GameObject bubbleFab;
    public int bubbleCount = 3;
    public float spawnRadius = 4f;
    private Transform playerTransform;
    //kitttyyyyyyyyy :3
    public KittyAI kitty;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   public IEnumerator SpawnBubbles()
    {
        Debug.Log("Start");
        for (int i = 0; i < bubbleCount; i++)
        {
            yield return new WaitForSeconds(1);
            Vector3 pos = playerTransform.position + new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0,
                Random.Range(-spawnRadius, spawnRadius));

            
            GameObject bubble = Instantiate(bubbleFab, pos, Quaternion.identity);
            Debug.Log(bubble.name);
          //  FishBubble fb = bubble.GetComponent<FishBubble>();
           // fb.river = this;
        }
  
    }
    public void StartCoroutineExternal(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        Debug.Log("Called coroutine external");
        StartCoroutine(SpawnBubbles());
    }
    public void FishSuccess()
    {
        kitty.CatchFish();
    }

    public void FishFail()
    {
        kitty.MissFish();
    }
}
