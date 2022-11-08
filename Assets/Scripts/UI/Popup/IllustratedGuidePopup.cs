using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustratedGuidePopup : TabPopup
{
    [SerializeField]
    private RectTransform[] contents;

    [SerializeField]
    private IllustratedGuidePrefab guidePrefab;

    private GuideParser parser;

    private bool isSettingEnd = false;
    public bool IsSettingEnd => isSettingEnd;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(OpenPopup());
    }

    private IEnumerator OpenPopup()
    {
        parser = GetComponent<GuideParser>();

        if (parser == null) yield break;

        yield return StartCoroutine(parser.Parse("GuideFile",SetPopupData));
    }

    private void SetPopupData(GuideData[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            GuideData data = datas[i];

            if (data.texts.Length > 0)
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
        Open();
    }
}
