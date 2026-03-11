using UnityEngine;

public class KittyFriend : MonoBehaviour
{
    public GameObject kittyTextUI;
    private bool win;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!win) kittyTextUI.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            win = true;
            kittyTextUI.SetActive(true);
        }
            
    }
}

