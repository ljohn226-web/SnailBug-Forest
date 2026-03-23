using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("TreeKitty").GetComponent<KittyFriend>().isCatSaved)
        {
            transform.position = new Vector3(165, 1, -108);
            Debug.Log("Player moved");
        }
        Debug.Log("IsCatSaved is " + GameObject.FindGameObjectWithTag("TreeKitty").GetComponent<KittyFriend>().isCatSaved);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
