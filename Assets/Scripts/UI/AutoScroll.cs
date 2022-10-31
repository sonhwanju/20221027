using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [SerializeField]
    private RectTransform prefab;

    [SerializeField]
    private RectTransform[] objs;

    private int initCount = 5;
    private int lastIdx = 0;

    private float width = 1080f;

    #region Coroutine

    private Coroutine autoScrollCoroutine = null;
    private WaitForSeconds autoScrollWs = null;
    private WaitForEndOfFrame waitForEndOfFrame = null;

    [SerializeField]
    private float autoScrollTime = 2.5f;
    #endregion

    private void Awake()
    {
        autoScrollWs = new WaitForSeconds(autoScrollTime);
        waitForEndOfFrame = new WaitForEndOfFrame();

    }

    private void Start()
    {
        width = Screen.width;
        objs = new RectTransform[initCount];
        lastIdx = objs.Length - 1;

        for (int i = 0; i < initCount; i++)
        {
            objs[i] = Instantiate(prefab, transform);
            SetAnchoredPos(objs[i], i * width);
        }
    }

    private void SetAnchoredPos(RectTransform obj,float width)
    {
        obj.offsetMin = new Vector2(width, obj.offsetMin.y);
        obj.offsetMax = new Vector2(width, obj.offsetMax.y);
    }

    private void MoveLeft(RectTransform obj)
    {
        obj.offsetMin += new Vector2(-width, obj.offsetMin.y);
        obj.offsetMax += new Vector2(-width, obj.offsetMax.y);
    }

    private void MoveRight(RectTransform obj)
    {
        obj.offsetMin += new Vector2(width, obj.offsetMin.y);
        obj.offsetMax += new Vector2(width, obj.offsetMax.y);
    }

    private void ResetAnchoredPosition(RectTransform obj)
    {
        obj.offsetMin = objs[lastIdx].offsetMin;
        obj.offsetMax = objs[lastIdx].offsetMax;

        MoveRight(obj);
    }

    public void ShiftObjs()
    {
        RectTransform obj = objs[0];

        Array.Copy(objs, 1, objs, 0, lastIdx);
        objs[lastIdx] = obj;
    }

    public void StartScroll()
    {
        autoScrollCoroutine = StartCoroutine(SwipeObjs());
    }

    public void StopScroll()
    {
        if (autoScrollCoroutine != null)
        {
            StopCoroutine(autoScrollCoroutine);
        }
    }


    private IEnumerator SwipeObjs()
    {
        yield return autoScrollWs;

        while (true)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                MoveLeft(objs[i]);
            }

            yield return autoScrollWs;

            ResetAnchoredPosition(objs[0]);
            ShiftObjs();
        }
    }
}
