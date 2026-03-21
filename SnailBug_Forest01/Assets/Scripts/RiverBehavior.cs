using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBehavior : MonoBehaviour
{

    [Header ("Bubbles")]
    public GameObject bubbleSmall;
    public GameObject bubbleLarge;
    public int bubbleCount = 4;
    public float spawnRadius = 2f;
    private List<GameObject> activeBubbles = new List<GameObject>();
    private Vector3 spawnCenter;

    private Collider riverCol; 
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
     
    public void SpawnSingleBubble()
    {
            Vector3 spawnPos = GetSpawnPointOnRiver(riverCol);

            //weighted choice between small + large buble to spawn; 30% large/70% small
            GameObject bubbleToSpawn = Random.value > 0.7f ? bubbleLarge : bubbleSmall;

            //instantiate chosen bubble and add to list
            GameObject bubble = Instantiate(bubbleToSpawn, spawnPos, Quaternion.identity);
            activeBubbles.Add(bubble);

            //start destruction coroutine
            StartCoroutine(DestroyBubblesAfterTime(bubble, 7f));
            Debug.Log("Spawned:  " + bubble.name);
    }
  
   public IEnumerator SpawnBubbles()
    {
        Debug.Log("Start");
       /* for (int i = 0; i < bubbleCount; i++)
        {
            yield return new WaitForSeconds(1);

                /*pos = spawnCenter + new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0.4f,
                Random.Range(-spawnRadius, spawnRadius));

            if (activeBubbles.Count >= 4)
            {
                Destroy(activeBubbles[0]);
                activeBubbles.RemoveAt(0);
            }
            //  FishBubble fb = bubble.GetComponent<FishBubble>();
            // fb.river = this; 
        } */

        while (true)
        {
           // Debug.Log("WHILE LOOP RUNNING");
            yield return new WaitForSeconds(2);
            if (activeBubbles.Count < 4)
            {
                SpawnSingleBubble();
            }
        }
  
    }

    public IEnumerator DestroyBubblesAfterTime(GameObject bubble, float time)
    {
        yield return new WaitForSeconds(time);

        if (bubble != null)
        {
            activeBubbles.Remove(bubble);
            Destroy(bubble);
        }
    }
    public void StartCoroutineExternal(Vector3 hitPoint, Collider riverCollider)
    {
        //need river collider 
        this.spawnCenter = hitPoint;
        this.riverCol = riverCollider;
        Debug.Log("Called coroutine external");
        StartCoroutine(SpawnBubbles());
    }

    Vector3 GetSpawnPointOnRiver(Collider col)
    {
        Bounds bounds = col.bounds;

        for (int i = 0; i < 5; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-spawnRadius, spawnRadius),
                0f, Random.Range(-spawnRadius, spawnRadius));

            Vector3 randomPoint = spawnCenter + offset;
            
            Vector3 closest = riverCol.ClosestPoint(randomPoint);
            //EASIER VERSION?
            /* //use bounds of river collider to...
             float randomX = Random.Range(bounds.min.x, bounds.max.x);
             float randomZ = Random.Range(bounds.min.z, bounds.max.z);

             //create random point 
             Vector3 randomPoint = new Vector3(randomX, bounds.max.y, randomZ);

             //closest point on the surface of collider
             Vector3 closest = col.ClosestPoint(randomPoint);

             //if random point is close enough to surface */
             if (Vector3.Distance(randomPoint, closest) < 0.3f)
             {
                  return closest; //return surface point 
             } 

        }
            Debug.Log("used backup spawnPoint");
            return spawnCenter; //fallback why wont you woooooorrrk 

    }

    public void RemoveBubble(GameObject bubble)
    {
        if (activeBubbles.Contains(bubble))
        {
            activeBubbles.Remove(bubble);
        }
    }

}
