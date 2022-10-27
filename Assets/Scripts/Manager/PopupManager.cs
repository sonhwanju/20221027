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
            Debug.LogError("PopupManager가 여러 개 있습니다.");
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

    
}
