using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabPopup : Popup
{
    [SerializeField]
    private CanvasGroup[] tabObjCgs;

    [SerializeField]
    private Button[] tabBtns;

    private void ResetTabs()
    {
        for (int i = 0; i < tabObjCgs.Length; i++)
        {
            UtilClass.SetCanvasGroup(tabObjCgs[i], false);
        }
    }

    private void TabBtnClickEvent(Transform t)
    {
        ResetTabs();
        UtilClass.SetCanvasGroup(tabObjCgs[t.GetSiblingIndex()], true);
    }

    public override void Open()
    {
        base.Open();
    }
}
