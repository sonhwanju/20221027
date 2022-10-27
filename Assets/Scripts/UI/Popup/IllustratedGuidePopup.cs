using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustratedGuidePopup : TabPopup
{
    [SerializeField]
    private RectTransform[] contents;

    [SerializeField]
    private AutoScroll[] autoScrolls;

    [SerializeField]
    private IllustratedGuidePrefab guidePrefab;

    private GuideParser parser;

    private bool isSettingEnd = false;
    public bool IsSettingEnd => isSettingEnd;

    protected override void Start()
    {
        base.Start();

        parser = GetComponent<GuideParser>();

        if (parser == null) return;

        GuideData[] datas = parser.Parse("GuideFile");

        for (int i = 0; i < datas.Length; i++)
        {
            GuideData data = datas[i];

            if(data.texts.Length > 0)
            {
                for (int j = 0; j < data.names.Length; j++)
                {
                    IllustratedGuidePrefab prefab = Instantiate(guidePrefab, contents[i]);
                    prefab.SetGuideText(data.names[j], data.texts[j]);
                }
            }
            else
            {
                for (int j = 0; j < data.names.Length; j++)
                {
                    IllustratedGuidePrefab prefab = Instantiate(guidePrefab, contents[i]);
                    prefab.SetGuideText(data.names[j]);
                }
            }
        }

        isSettingEnd = true;
    }

    private void ResetScroll()
    {
        for (int i = 0; i < autoScrolls.Length; i++)
        {
            autoScrolls[i].StopScroll();
        }
    }

    protected override void TabBtnClickEvent(Transform t)
    {
        ResetScroll();
        base.TabBtnClickEvent(t);
        autoScrolls[t.GetSiblingIndex()].StartScroll();
    }

    public override void Close()
    {
        base.Close();
        ResetScroll();
    }
}
