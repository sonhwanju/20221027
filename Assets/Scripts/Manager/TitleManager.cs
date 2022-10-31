using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI startText;

    [SerializeField]
    private float fadeTime = 0.8f;

    private void Start()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        bool fadeIn = false;
        while (true)
        {
            Coroutine c = StartCoroutine(UtilClass.Fade(startText, fadeTime, fadeIn));
            yield return new WaitUntil(() => fadeIn ? (startText.color.a >= 1f) : (startText.color.a <= 0f));
            fadeIn = !fadeIn;
        }
    }
}
