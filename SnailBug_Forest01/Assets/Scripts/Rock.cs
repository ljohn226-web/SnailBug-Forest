using TMPro;
using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour
{
    [Header("References")]
    public PlayerMovement pm;
    public Transform cam;
    public LayerMask whatIsRock;

    [Header("Counter idk")]
    public int totalRocksInteractedWith;
    public int rocksForWin;

    [Header("UI")]
    public TextMeshProUGUI intText;
    private TextMeshProUGUI actionText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        actionText = GameObject.FindGameObjectWithTag("ActionText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(IntroText());
    }

    // Update is called once per frame
    void Update()
    {
        //send out a raycast to check if rocks are in the player's view
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 5f, whatIsRock))
        {
            //if in view, make alpha back to visible
            intText.color = new Color(intText.color.r, intText.color.g, intText.color.b, 1);
            if (Input.GetKey(KeyCode.E))
            {
                if (totalRocksInteractedWith < rocksForWin)
                {
                    totalRocksInteractedWith++;
                    hit.collider.GetComponent<RockBehavior>().interactedWith = true;
                    hit.collider.gameObject.layer = 3;
                }
                else hit.collider.GetComponent<RockBehavior>().win = true;
            }
        }
        else
        {
            intText.color = new Color(1, 1, 1, 0);
        }
    }
    IEnumerator IntroText()
    {
        actionText.text = "Beaver said Spider can help us. Spider lives in this area.";
        yield return new WaitForSeconds(10f);
        actionText.text = "Maybe under one of these rocks?";
    }
}
