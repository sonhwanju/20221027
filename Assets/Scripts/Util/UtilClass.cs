using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class UtilClass
{
    public static void SetCanvasGroup(CanvasGroup cg, bool on)
    {
        cg.alpha = on ? 1f : 0f;
        cg.blocksRaycasts = on;
    }

    public static IEnumerator FadeCanvasGroup(CanvasGroup cg, float fadeTime, bool fadeIn)
    {
        float timer = 0f;

        while (timer < fadeTime)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            if(fadeIn)
            {
                cg.alpha = Mathf.Clamp01(timer / fadeTime); ;
            }
            else
            {
                cg.alpha = 1 - Mathf.Clamp01(timer / fadeTime);
            }
        }

        if(!fadeIn)
        {
            cg.blocksRaycasts = false;
        }
    }

    public static IEnumerator Fade<T>(T image, float fadeTime, bool fadeIn) where T : Graphic
    {
        float elapsedTime = 0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.unscaledDeltaTime;
            if (fadeIn)
            {
                c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            }
            else
            {
                c.a = 1f - Mathf.Clamp01(elapsedTime / fadeTime);
            }

            image.color = c;
        }
    }

    public static T CreateObj<T>(string path) where T : Object
    {
        T obj = Resources.Load<T>(path);

        return GameObject.Instantiate(obj);
    }
}
