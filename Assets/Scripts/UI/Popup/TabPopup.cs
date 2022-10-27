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

    private void Start()
    {
        for (int i = 0; i < tabBtns.Length; i++)
        {
            int idx = i;
            tabBtns[idx].onClick.AddListener(() => TabBtnClickEvent(tabBtns[idx].transform));
        }
    }

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
        ResetTabs();
        tabBtns[0].onClick?.Invoke();
        tabBtns[0].Select();

        base.Open();
    }
}
