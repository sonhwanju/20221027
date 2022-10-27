using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilClass
{
    public static void SetCanvasGroup(CanvasGroup cg, bool on)
    {
        cg.alpha = on ? 1f : 0f;
        cg.interactable = on;
        cg.blocksRaycasts = on;
    }
}
