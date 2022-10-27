using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    private Dictionary<string, Popup> popupDictionary = new Dictionary<string, Popup>();
    private Stack<Popup> popupStack = new Stack<Popup>();

    [SerializeField]
    private Popup[] popupPrefabs;

    [SerializeField]
    private CanvasGroup popupParent;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("PopupManager�� ���� �� �ֽ��ϴ�.");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < popupPrefabs.Length; i++)
        {
            popupDictionary.Add(popupPrefabs[i].PopupKey, Instantiate(popupPrefabs[i], popupParent.transform));
        }
    }

    public void OpenPopup(string key)
    {
        if(popupStack.Count <= 0) //�ƹ��͵� �� �����ִ� ����
        {
            UtilClass.SetCanvasGroup(popupParent, true);
        }

        popupStack.Push(popupDictionary[key]);
        popupDictionary[key].Open();
    }

    public void ClosePopup()
    {
        if (popupStack.Count <= 0) return;

        popupStack.Pop().Close();

        if(popupStack.Count <= 0) //������ �˾��� ���� ����
        {
            UtilClass.SetCanvasGroup(popupParent, false);
        }
    }
}
