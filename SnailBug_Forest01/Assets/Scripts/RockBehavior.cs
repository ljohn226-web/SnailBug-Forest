using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockBehavior : MonoBehaviour
{
    public bool interactedWith;
    public bool win;
    private int hurtOrHeal;
    private TextMeshProUGUI actionText;

    public PlayerMovement pm;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionText = GameObject.FindGameObjectWithTag("ActionText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (interactedWith)
        {
            Debug.Log("Interacted with and triggered in rock behavior script");

            hurtOrHeal = Random.Range(0, 2); //0 or 1... 0 is hurt and 1 is heal.

            Debug.Log("Hurt or heal value is " + hurtOrHeal);

            if (hurtOrHeal == 0)
            {
                HurtRock();
                interactedWith = false;

                Debug.Log("Rock has been deemed hurt.");
                Debug.Log("interactedWith is " + interactedWith);
            }
            if (hurtOrHeal == 1)
            {
                HealRock();
                interactedWith = false;

                Debug.Log("Rock has been deemed heal.");
                Debug.Log("interactedWith is " + interactedWith);
            }
        }
        if (win)
        {
            WinRock();
            win = false;
        }
    }

    public void HurtRock()
    {
        actionText.text = "Ouch! A centipede under this rock bit you.";
        pm.ApplyDamage(20f);
        StartCoroutine(Wait(5f));
    }
    
    public void HealRock()
    {
        actionText.text = "Snack time! You eat the crickets under this rock.";
        if (pm.playerHealth < 80) pm.playerHealth =+ 20;
        else pm.playerHealth = 100;
        StartCoroutine(Wait(5f));
    }

    public void WinRock()
    {
        actionText.text = "Someone is wriggling from underneath an even smaller rock...!";
        StartCoroutine(Wait(8f));
        SceneManager.LoadScene("StorySceneSpiderToCat");
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        actionText.text = " ";
    }
}
