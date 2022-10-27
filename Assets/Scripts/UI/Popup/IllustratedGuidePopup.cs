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

    protected override void Start()
    {
        base.Start();

        parser = GetComponent<GuideParser>();

        if (parser == null) return;

        GuideData[] datas = parser.Parse("GuideFile");

        for (int i = 0; i < datas.Length; i++)
        {
            GuideData data = datas[i];
            for (int j = 0; j < data.names.Length; j++)
            {
                IllustratedGuidePrefab prefab = Instantiate(guidePrefab, contents[i]);
                prefab.SetGuideText(data.names[j]);
            }
        }
    }
}
