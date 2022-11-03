using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    private Dictionary<string, Popup> popupDictionary = new Dictionary<string, Popup>();
    private Stack<Popup> popupStack = new Stack<Popup>();

    [SerializeField]
    private CanvasGroup popupParent;

    private void Awake()
    {
        if(Instance != null)
        {
            Logger.LogError("PopupManager가 여러 개 있습니다.");
            return;
        }
        Instance = this;
    }

    public void OpenPopup(Popup prefab)
    {
        Profiler.BeginSample("OpenPopup");
        string key = prefab.PopupKey;

        if(!popupDictionary.ContainsKey(key))
        {
            popupDictionary.Add(key, Instantiate(prefab,popupParent.transform));
        }

        if (popupStack.Count <= 0) //아무것도 안 열려있는 상태
        {
            UtilClass.SetCanvasGroup(popupParent, true);
        }

        Popup p = popupDictionary[key];

        popupStack.Push(p);
        p.transform.SetAsLastSibling();
        p.Open();
        Profiler.EndSample();
    }

    public Popup OpenPopup(Popup prefab, bool isOpen = false)
    {
        string key = prefab.PopupKey;

        if (!popupDictionary.ContainsKey(key))
        {
            popupDictionary.Add(key, Instantiate(prefab, popupParent.transform));
        }

        if (popupStack.Count <= 0) //아무것도 안 열려있는 상태
        {
            UtilClass.SetCanvasGroup(popupParent, true);
        }

        Popup p = popupDictionary[key];

        popupStack.Push(p);
        p.transform.SetAsLastSibling();

        if(isOpen)
            p.Open();

        return p;
    }

    public void ClosePopup()
    {
        if (popupStack.Count <= 0) return;

        popupStack.Pop().Close();

        if(popupStack.Count <= 0) //마지막 팝업이 닫힌 상태
        {
            UtilClass.SetCanvasGroup(popupParent, false);
        }
    }
}
