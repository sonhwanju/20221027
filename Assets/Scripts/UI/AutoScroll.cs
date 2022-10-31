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

    private Color[] objColor = null;

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

        objColor = new Color[]
        {
            Color.black,Color.red,Color.blue,Color.yellow, Color.green
        };
    }

    private void Start()
    {
        width = Screen.width;
        objs = new RectTransform[initCount];
        lastIdx = objs.Length - 1;

        for (int i = 0; i < initCount; i++)
        {
            objs[i] = Instantiate(prefab, transform);
            objs[i].GetComponent<Image>().color = objColor[i];
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

    private IEnumerator SmoothMoveLeft(RectTransform obj, float lerpTime = 1f)
    {
        Vector2 targetMin = obj.offsetMin + new Vector2(-width, obj.offsetMin.y);
        Vector2 targetMax = obj.offsetMax + new Vector2(-width, obj.offsetMax.y);

        float timer = 0f;

        while (timer < lerpTime)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            obj.offsetMin = Vector2.Lerp(obj.offsetMin, targetMin, timer / lerpTime);
            obj.offsetMax = Vector2.Lerp(obj.offsetMax, targetMax, timer / lerpTime);
        }

        obj.offsetMin = targetMin;
        obj.offsetMax = targetMax;
    }

    private IEnumerator SwipeObjs()
    {
        yield return autoScrollWs;

        while (true)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                StartCoroutine(SmoothMoveLeft(objs[i],1f));
            }

            yield return autoScrollWs;

            ResetAnchoredPosition(objs[0]);
            ShiftObjs();
        }
    }
}
