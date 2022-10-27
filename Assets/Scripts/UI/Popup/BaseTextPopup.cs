using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BaseTextPopup : Popup
{
    [SerializeField]
    private TextMeshProUGUI headerText;
    [SerializeField]
    private TextMeshProUGUI msgText;

    public BaseTextPopup SetHeaderText(string text)
    {
        headerText.text = text;

        return this;
    }

    public BaseTextPopup SetMsgText(string text)
    {
        msgText.text = text;

        return this;
    }

    private void InitText()
    {
        headerText.text = "";
        msgText.text = "";
    }

    public override void Close()
    {
        base.Close();
        InitText();
    }
}
