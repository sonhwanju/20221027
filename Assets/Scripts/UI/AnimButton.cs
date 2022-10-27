using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimButton : Button
{
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
