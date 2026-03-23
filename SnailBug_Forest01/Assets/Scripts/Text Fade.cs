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
            textList.Add("CAT: Spider...?");
            textList.Add("SPIDER: Are you alright, Cat?");
            textList.Add("SPIDER: What happened here?");
            textList.Add("CAT: The humans have been cutting down all the trees!");
            textList.Add("CAT: They have these big monsters that chop them down and pick up the logs.");
            textList.Add("CAT: They were so loud, and I...");
            textList.Add("LIZARD: You came running up that tree?");
            textList.Add("CAT: Yeah.");
            textList.Add("CAT: Wait, who are you?");
            textList.Add("LIZARD: I'm Lizard. I'm friends with Beaver.");
            textList.Add("SPIDER: Lizard helped me when I was trapped under a rock.");
            textList.Add("CAT: Nice to meet you, Lizard. What brings you up here?");
            textList.Add("LIZARD: Well, I was hoping you could help us, help Beaver.");
            textList.Add("LIZARD: We need help finding Bear.");
            textList.Add("LIZARD: She's the only one who can help Beaver rebuild.");
            textList.Add("SPIDER: The humans fell a tree that destroyed Beaver's dam!");
            textList.Add("CAT: That's certainly rough.");
            textList.Add("CAT: I know where Bear went to hibernate, but...");
            textList.Add("CAT: ...she isn't gonna be happy being woken up.");
            textList.Add("LIZARD: Ah, well...");
            textList.Add("SPIDER: Ah, but Cat, don't you fish?");
            textList.Add("CAT: Second best, only beaten by Bear.");
            textList.Add("SPIDER: Maybe we can give her a present! That's sure to make her happy.");
            textList.Add("CAT: I don't know. I'm still pretty shaken up.");
            textList.Add("SPIDER: Will...");
            textList.Add("Spider makes a small ball of silk.");
            textList.Add("SPIDER: ...this convince you?");
            textList.Add("CAT: BALL! STRING! SILK! BALL!");
            textList.Add("Cat playfully pounces onto it.");
            textList.Add("SPIDER: Hehehe. Just as excited as when I met you.");
            textList.Add("CAT: Why, you... fine, I'm in.");
            textList.Add("LIZARD: Yay! Is there a river on the way?");
            textList.Add("CAT: Yep. It's pretty rough, but with the right timing, fishing is a breeze.");
            textList.Add("LIZARD: Lead the way!");
            nextSceneName = "Save Cat Level";
        }
        if (SceneManager.GetActiveScene().name == "WinScene")
        {
            textList.Clear();
            textList.Add("BEAR: Yawn...");
            textList.Add("BEAR: Who...");
            textList.Add("BEAR: Who DARES to wake me up from my hibernation.");
            textList.Add("CAT: Uh oh.");
            textList.Add("SPIDER: Eeek!");
            textList.Add("BEAR: It's barely spring yet!");
            textList.Add("LIZARD: Excuse me, miss.");
            textList.Add("BEAR: Was it YOU?");
            textList.Add("SPIDER: Lizard, no...!");
            textList.Add("LIZARD: Yes. I've come with my friends to ask for your help.");
            textList.Add("BEAR: Grrrrrrrrr...");
            textList.Add("The cave shakes with Bear's loud growl.");
            textList.Add("CAT: Well, I've got another 8 lives anyway...");
            textList.Add("CAT: I've got some fish for you, Bear. Sorry for waking you up.");
            textList.Add("BEAR: ...");
            textList.Add("Bear sniffs at the fish and her expression softens.");
            textList.Add("BEAR: You... need my help?");
            textList.Add("LIZARD: Yes, ma'am. Beaver's dam was broken and needs fixing.");
            textList.Add("SPIDER: ...The humans. They keep destroying the forest...");
            textList.Add("BEAR: Bah. Those darned humans...");
            textList.Add("BEAR: Beaver is my friend. I will help.");
            textList.Add("LIZARD: Thank you so much! Whew, what a journey...");
            textList.Add("BEAR: Friends of my friends, are also friends.");
            textList.Add("LIZARD: We are... friends?");
            textList.Add("BEAR: Yawn... yes, little one. We are friends.");
            textList.Add("Bear snacks on the gifted fish.");
            textList.Add("Bear is now our friend.");
            nextSceneName = "MainMenu";
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
            FadeOut();
            textCounter++;
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

