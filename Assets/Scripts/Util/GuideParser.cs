using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GuideParser : MonoBehaviour
{
    private char[] splitChar;

    List<GuideData> dataList = new List<GuideData>();

    List<string> nameList = new List<string>();
    List<string> textList = new List<string>();

    private AsyncOperationHandle<TextAsset> handle;

    private void Awake()
    {
        splitChar = new char[] { ',' };
    }

    public IEnumerator Parse(string fileName,Action<GuideData[]> callback)
    {
        handle = Addressables.LoadAssetAsync<TextAsset>(fileName);
        
        //yield return handle;
        LoadSceneManager.Instance.StartLoading();

        while (!handle.IsDone)
        {
            LoadSceneManager.Instance.SetLoadingText(handle.PercentComplete * 100);
            yield return null;
        }

        LoadSceneManager.Instance.EndLoading();

        TextAsset textAsset = handle.Result;

        string[] data = textAsset.text.Split(new char[] { '\n' });
        string[] row = new string[] { };

        dataList.Clear();
        for (int i = 1; i < data.Length;) //0은 데이터가 아님
        {
            row = data[i].Split(splitChar);
            GuideData guideData = new GuideData();

            nameList.Clear();
            textList.Clear();
            do
            {
                nameList.Add(row[1]);
                if (row.Length > 2)
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
        Addressables.Release(handle);
        callback?.Invoke(dataList.ToArray());
    }
}
