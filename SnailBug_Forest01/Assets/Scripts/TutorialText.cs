using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class TutorialText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fadeTime;
    private float timePassed = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTime > timePassed)
        {
            timePassed += Time.deltaTime;
            float percentage = timePassed / fadeTime;
            Color startColor = new Color(text.color.r, text.color.g, text.color.b, 1); // opaque alpha
            Color endColor = new Color(text.color.r, text.color.g, text.color.b, 0);   // transparent alpha
            //lerp from opaque to transparent
            //Debug.Log("Lerping");
            //Debug.Log("Percentage:" + percentage);
            text.color = Color.Lerp(startColor, endColor, percentage);
        }
    }
}