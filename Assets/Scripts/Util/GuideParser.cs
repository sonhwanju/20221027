using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideParser : MonoBehaviour
{
    private char[] splitChar;

    private void Awake()
    {
        splitChar = new char[] { ',' };
    }

    public GuideData[] Parse(string fileName)
    {
        List<GuideData> dataList = new List<GuideData>();

        TextAsset textAsset = Resources.Load<TextAsset>(fileName);

        string[] data = textAsset.text.Split(new char[] { '\n' });
        string[] row = new string[] { };
        List<string> nameList = new List<string>();
        List<string> textList = new List<string>();

        for (int i = 1; i < data.Length;) //0은 데이터가 아님
        {
            row = data[i].Split(splitChar);
            GuideData guideData = new GuideData();

            nameList.Clear();
            textList.Clear();
            do
            {
                nameList.Add(row[1]);
                if(row.Length > 2)
                {
                    textList.Add(row[2]);
                }

                if (++i < data.Length)
                {
                    row = data[i].Split(splitChar);
                }
                else break;
            } while (row[0].ToString().Equals(""));

            guideData.names = nameList.ToArray();
            guideData.texts = textList.ToArray();
            dataList.Add(guideData);
        }

        return dataList.ToArray();
    }
}
