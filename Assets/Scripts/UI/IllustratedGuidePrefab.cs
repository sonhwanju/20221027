using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IllustratedGuidePrefab : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI guideText;

    private Button btn;

    private PopupData popupData;

    private string text;

    private void Awake()
    {
        btn = GetComponent<Button>();
        popupData = GetComponent<PopupData>();
    }

    private void Start()
    {

        btn.onClick.AddListener(() =>
        {
            ((BaseTextPopup)PopupManager.Instance.OpenPopup(popupData.PopupPrefab, false))
            .SetHeaderText(guideText.text)
            .SetMsgText(text)
            .Open();
        });
    }

    public void SetGuideText(string msg, string text = null)
    {
        guideText.text = msg;
        this.text = text;

        btn.enabled = text != null;
    }
}
