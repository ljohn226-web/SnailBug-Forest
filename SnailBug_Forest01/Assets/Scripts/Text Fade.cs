using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<string> textList = new List<string>();
    private int textCounter = 0;
    private string nextSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (SceneManager.GetActiveScene().name == "Intro Scene")
        {
            textList.Add("The forest is home to many of our friends.");
            textList.Add("Birds, bugs, bears, beavers, bass...");
            textList.Add("It's my home, too.");
            textList.Add("Yesterday, a big group of humans came.");
            textList.Add("They knocked down so many trees!");
            textList.Add("Many homes were destroyed.");
            textList.Add("Our friend beaver needs help now.");
            textList.Add("Beaver's home was destroyed by a fallen tree!");
            textList.Add("But only one animal in the forest");
            textList.Add("has the strength to help repair it.");
            textList.Add("We must journey out...");
            textList.Add("and BEFRIEND BEAR.");
            nextSceneName = "Save Cat Level";
            //SET THAT TO SAVE SPIDER LEVEL
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && textCounter < textList.Count) 
        {
            FadeOut();
            textCounter++;
        }
        if (Input.anyKeyDown && textCounter > textList.Count)
        {
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("End of text. Add transition scene here");
        }
    }
    public void FadeOut()
    {
        StartCoroutine(ChangeColor(Color.black, Color.white, 1f));
        FadeIn(textList[textCounter]);
    }

    IEnumerator ChangeColor(Color startColor, Color endColor, float duration)
    {
        float tick = 0f;

        while (tick < 1f)
        {
            //color updates every frame
            text.color = Color.Lerp(startColor, endColor, tick);

            //increase tick based on time passed since last frame
            tick += Time.deltaTime / duration;

            yield return null;
        }
        text.color = endColor;
    }
    public void FadeIn(string newText)
    {
        StartCoroutine(ChangeColor(Color.white, Color.black, 1));
        text.text = newText;
    }
}
