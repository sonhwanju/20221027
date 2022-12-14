using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabPopup : Popup
{
    [SerializeField]
    protected CanvasGroup[] tabObjCgs;

    [SerializeField]
    protected Button[] tabBtns;

    protected override void Start()
    {
        base.Start();

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

    protected virtual void TabBtnClickEvent(Transform t)
    {
        ResetTabs();
        UtilClass.SetCanvasGroup(tabObjCgs[t.GetSiblingIndex()], true);
    }

    public override void Open()
    {
        tabBtns[0].onClick?.Invoke();
        tabBtns[0].Select();

        base.Open();
    }
}
