using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    private ScrollRect scrollRect;

    private RectTransform content;

    private Vector2 anchoredPosition = Vector2.zero;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
    }

    private void Start()
    {
        StartCoroutine(Frame());
    }

    private void SnapTo()
    {
        Canvas.ForceUpdateCanvases();

        content.DOAnchorPos(anchoredPosition, 1f).OnComplete(() =>
         {
             content.anchoredPosition = new Vector2(content.anchoredPosition.x, 0f);
             content.GetChild(0).SetSiblingIndex(content.childCount - 1);

             SnapTo();
         });
    }

    private IEnumerator Frame()
    {
        yield return new WaitForEndOfFrame();

        Canvas.ForceUpdateCanvases();

        RectTransform target = content.GetChild(1).GetComponent<RectTransform>();
        //anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
        anchoredPosition = scrollRect.transform.InverseTransformPoint(target.position);
    }
}
