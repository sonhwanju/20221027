using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    protected CanvasGroup cvs;

    protected virtual void Awake()
    {
        cvs = GetComponent<CanvasGroup>();

        Close();
    }

    public virtual void Open()
    {
        cvs.alpha = 1f;
        cvs.blocksRaycasts = true;
        cvs.interactable = true;
    }

    public virtual void Close()
    {
        cvs.alpha = 0f;
        cvs.blocksRaycasts = false;
        cvs.interactable = false;
    }
}