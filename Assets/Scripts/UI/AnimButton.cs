using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimButton : OnClickSoundButton
{
    private PopupData popupData = null;

    protected override void Awake()
    {
        base.Awake();

        popupData = GetComponent<PopupData>();

        if (popupData)
        {
            popupData.SetButtonEvent(this);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        animator.SetTrigger("Disabled");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        animator.SetTrigger("Enabled");
    }
}
