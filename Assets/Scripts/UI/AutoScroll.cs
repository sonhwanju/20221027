using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���� ��ũ�� �ڵ�
/// </summary>
public class AutoScroll : MonoBehaviour
{
    private ScrollRect scrollRect;

    private Transform[] contentChilds = new Transform[0];
    private RectTransform content;

    private Vector2 anchoredPosition = Vector2.zero;

    #region Coroutine

    private Coroutine co = null;
    private WaitForSeconds startWs = null;

    private float startTime = 1f;
    #endregion

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        startWs = new WaitForSeconds(startTime);

        content = scrollRect.content;
    }

    private void Start()
    {
        StartCoroutine(Frame());
    }

    private void SnapTo()
    {
        Canvas.ForceUpdateCanvases();
        content.DOAnchorPos(anchoredPosition, 1f).OnComplete(OnCompleteMove);
    }

    private void OnCompleteMove()
    {
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, 0f);
        content.GetChild(0).SetSiblingIndex(content.childCount - 1);

        SnapTo();
    }

    private IEnumerator Frame()
    {
        IllustratedGuidePopup popup = FindObjectOfType<IllustratedGuidePopup>();
        yield return new WaitUntil(() => popup.IsSettingEnd);

        Canvas.ForceUpdateCanvases();

        contentChilds = new Transform[content.childCount];
        for (int i = 0; i < content.childCount; i++)
        {
            contentChilds[i] = content.GetChild(i);
        }

        RectTransform target = contentChilds[1].GetComponent<RectTransform>();
        //anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
        anchoredPosition = scrollRect.transform.InverseTransformPoint(target.position);
    }

    public void StartScroll()
    {
        //SnapTo();
        co = StartCoroutine(Scroll());
    }

    public void StopScroll()
    {
        content.DOKill(true);

        if(co != null)
        {
           StopCoroutine(co);
        }

        InitChild();
    }

    private IEnumerator Scroll()
    {
        yield return startWs;

        SnapTo();
    }
}
