using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("PopupManager가 여러 개 있습니다.");
            return;
        }
        Instance = this;
    }
}
