using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IllustratedGuidePrefab : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI guideText;


    private string text;



    public void SetGuideText(string msg, string text = null)
    {
        guideText.text = msg;
        this.text = text;

    }
}
