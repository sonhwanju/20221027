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
        cg.interactable = on;
        cg.blocksRaycasts = on;
    }

    public static IEnumerator Fade<T>(T image, float fadeTime, bool fadeIn) where T : Graphic
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
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
}
