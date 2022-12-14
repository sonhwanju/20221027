using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnClickSoundButton : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.PlayOneShot("event:/ButtonClick");
        base.OnPointerClick(eventData);
    }
}
