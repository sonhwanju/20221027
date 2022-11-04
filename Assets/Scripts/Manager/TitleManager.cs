using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI startText;

    [SerializeField]
    private float fadeTime = 0.8f;

    private bool isOnce = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        StartCoroutine(FadeCoroutine());
    }

    private void Update()
    {
        if (isOnce) return;

#if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            //LoadScene
            //LoadSceneManager.Instance.LoadScene("MainScene");

            StartCoroutine(LoadSceneManager.Instance.LoadSceneCoroutine("MainScene"));
            isOnce = true;
        }

#else
        if(Input.touchCount > 0)
        {
            //LoadScene
            LoadSceneManager.Instance.LoadScene("MainScene");
            isOnce = true;
        }

#endif
    }

    private IEnumerator FadeCoroutine()
    {
        bool fadeIn = false;
        while (true)
        {
            yield return StartCoroutine(UtilClass.Fade(startText, fadeTime, fadeIn));
            fadeIn = !fadeIn;
        }
    }

    private void OnDestroy()
    {
    }
}
