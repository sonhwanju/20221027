using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SoundManager>();

                if (obj == null)
                {
                    instance = UtilClass.CreateObj<SoundManager>("SoundManager");
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

    private EventInstance bgmInstance;

    [SerializeField]
    private string bgmVca = "vca:/BGM";
    [SerializeField]
    private string sfxVca = "vca:/Sfx";
    [SerializeField]
    private string bgmEvent = "event:/BGM";

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitVcaSound();
        CreateBgmInstance();
        StartSound(bgmInstance);
    }

    public static void PlayOneShot(string path)
    {
        RuntimeManager.PlayOneShot(path);
    }

    public static void StopSound(EventInstance instance, FMOD.Studio.STOP_MODE mode)
    {
        instance.stop(mode);
        instance.release();
    }

    public static void StartSound(EventInstance instance)
    {
        instance.start();
    }

    public void SetBgm(float value)
    {
        StopSound(bgmInstance,FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        bgmInstance.setParameterByName("Change", value);
        StartSound(bgmInstance);
    }

    public void SetBgmLoop(bool isLoop)
    {
        bgmInstance.setParameterByName("IsLoop", isLoop ? 1f : 0f);
    }

    private void CreateBgmInstance()
    {
        bgmInstance = RuntimeManager.CreateInstance(bgmEvent);
    }

    private void InitVcaSound()
    {
        SaveData data = DataManager.Instance.SaveData;

        if(data != null)
        {
            RuntimeManager.GetVCA(bgmVca).setVolume(data.bgmVolume);
            RuntimeManager.GetVCA(sfxVca).setVolume(data.sfxVolume);
        }
    }
}
