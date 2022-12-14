using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    protected CanvasGroup cvs;

    [SerializeField]
    protected string popupKey;
    public string PopupKey => popupKey;

    [SerializeField]
    protected Button exitBtn;
    public Button ExitBtn => exitBtn;

    protected virtual void Awake()
    {
        cvs = GetComponent<CanvasGroup>();

        Close();

    }

    protected virtual void Start()
    {
        exitBtn.onClick.AddListener(() => PopupManager.Instance.ClosePopup());
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