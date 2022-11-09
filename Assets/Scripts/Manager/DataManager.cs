using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<DataManager>();

                if (obj == null)
                {
                    instance = UtilClass.CreateObj<DataManager>("DataManager");
                }
                else
                {
                    instance = obj;
                }
            }
            return instance;
        }

        set => instance = value;
    }

    private SaveData saveData;
    public SaveData SaveData
    {
        get => saveData;
        set
        {
            saveData = value;
            SaveDataToJson();
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadDataFromJson();
    }

    public void SaveDataToJson()
    {
        string jsonData = JsonUtility.ToJson(saveData, true);
        string path = Path.Combine(Application.persistentDataPath, "gameData.json");
        File.WriteAllText(path, jsonData);
    }

    public void LoadDataFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "gameData.json");
        string jsonData = File.ReadAllText(path);
        saveData = JsonUtility.FromJson<SaveData>(jsonData);
    }
}

[System.Serializable]
public class SaveData
{
    public float bgmVolume;
    public float sfxVolume;

    public SaveData SetBgmVolume(float value)
    {
        bgmVolume = value;
        return this;
    }

    public SaveData SetSfxVolume(float value)
    {
        sfxVolume = value;
        return this;
    }
}