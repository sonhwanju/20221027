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
            Debug.LogError("PopupManager�� ���� �� �ֽ��ϴ�.");
            return;
        }
        Instance = this;
    }
}
