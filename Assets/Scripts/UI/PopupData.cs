using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupData : MonoBehaviour
{
    [SerializeField]
    private Popup popupPrefab;
    public Popup PopupPrefab => popupPrefab;

    public void SetButtonEvent(Button button)
    {
        button.onClick.AddListener(() => PopupManager.Instance.OpenPopup(popupPrefab));
    }

    public void SetButtonEvent(Button button ,bool isOpen)
    {
        button.onClick.AddListener(() => PopupManager.Instance.OpenPopup(popupPrefab, isOpen));
    }
}
