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

    private void CreateBgmInstance()
    {
        bgmInstance = RuntimeManager.CreateInstance(bgmEvent);
    }
}
