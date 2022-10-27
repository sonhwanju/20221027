using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IllustratedGuidePrefab : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI guideText;

    public void SetGuideText(string msg)
    {
        guideText.text = msg;
    }
}
