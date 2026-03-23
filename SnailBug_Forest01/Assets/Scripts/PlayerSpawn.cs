using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("TreeKitty").GetComponent<KittyFriend>().isCatSaved)
        {
            StartCoroutine(TeleportPlayer());
        }
        Debug.Log("IsCatSaved is " + GameObject.FindGameObjectWithTag("TreeKitty").GetComponent<KittyFriend>().isCatSaved);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(1f);
        transform.position = new Vector3(170, 1, -108);
        Debug.Log("Player moved: " + transform.position);
    }
}
