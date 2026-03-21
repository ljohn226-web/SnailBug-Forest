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
            textList.Clear();
            textList.Add("The forest is home to many of our friends.");
            textList.Add("Birds, bugs, bears, beavers, bass...");
            textList.Add("It's my home, too.");
            textList.Add("Yesterday, a big group of humans came.");
            textList.Add("They knocked down lots of trees");
            textList.Add("and many of our homes were destroyed.");
            textList.Add("Our dear friend Beaver's dam is one such case.");
            textList.Add("The tree near their home fell and smashed it all up!");
            textList.Add("Beaver is okay, but their home is not.");
            textList.Add("There is one animal in the forest");
            textList.Add("who has the strength to help repair Beaver's home");
            textList.Add("and the homes of all our other friends.");
            textList.Add("She has been hibernating over the long winter,");
            textList.Add("so I have no idea where she is!");
            textList.Add("I, Lizard, now have one goal.");
            textList.Add("I've gotta get out there");
            textList.Add("and BEFRIEND BEAR.");
            nextSceneName = "SaveSpiderLevel";
        }
        if (SceneManager.GetActiveScene().name == "StorySceneSaveCatToRiver")
        {
            textList.Clear();
            textList.Add("CAT: Spider! It's good to see you!");
            textList.Add("SPIDER: It's nice to see you too, Cat.");
            textList.Add("SPIDER: What happened here?");

        }
        if (SceneManager.GetActiveScene().name == "StorySceneSpiderToCat")
        {
            textList.Clear();
            textList.Add("SPIDER: Whew! I nearly suffocated under there!");
            textList.Add("SPIDER: Thanks for helping me!");
            textList.Add("SPIDER: What is your name, scaly creature?");
            textList.Add("LIZARD: I'm Lizard! I'm one of Beaver's other friends.");
            textList.Add("SPIDER: Beaver? You must be real nice, then!");
            textList.Add("SPIDER: What brings you around this part of the forest?");
            textList.Add("SPIDER: Besides saving my life, of course.");
            textList.Add("LIZARD: See, I've actually been looking for you.");
            textList.Add("LIZARD: Beaver's dam was destroyed!");
            textList.Add("SPIDER: Oh no! What happened?");
            textList.Add("LIZARD: Humans knocked down a tree the other day, and it fell on the dam.");
            textList.Add("SPIDER: Ah, those pesky humans...");
            textList.Add("SPIDER: How can I help? I'm pretty good at weaving!");
            textList.Add("LIZARD: Well, that's very nice, but we need to find Bear.");
            textList.Add("SPIDER: Oh. That makes sense.");
            textList.Add("SPIDER: I'm not sure where she hibernated, but...");
            textList.Add("SPIDER: I think I know someone who might!");
            textList.Add("LIZARD: That's awesome! Could you lead the way?");
            textList.Add("SPIDER: Yeah, I think I at least know what part of the forest they're in.");
            textList.Add("SPIDER: Follow me!");
            nextSceneName = "Save Cat Level";
        }
        FadeOut();
        textCounter++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && textCounter < textList.Count) 
        {
            FadeOut();
            textCounter++;
        }
        if (Input.anyKeyDown && textCounter == textList.Count)
        {
            StartCoroutine(DelayedSceneTransition());
            Debug.Log("End of text. Add transition scene here");
        }
    }
    public void FadeOut()
    {
        StartCoroutine(ChangeColor(Color.black, Color.white, 1f));
        FadeIn(textList[textCounter]);
    }

    IEnumerator DelayedSceneTransition()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextSceneName);
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
