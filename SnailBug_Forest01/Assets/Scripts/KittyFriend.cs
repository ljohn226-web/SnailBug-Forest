using UnityEngine;

public class KittyFriend : MonoBehaviour
{
    public GameObject kittyTextUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        kittyTextUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            kittyTextUI.SetActive(true);
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}

