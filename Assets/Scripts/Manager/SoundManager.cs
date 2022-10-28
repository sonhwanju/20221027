using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField]
    private List<AudioSource> sfxSourceList = new List<AudioSource>();

    [SerializeField]
    private AudioClip btnClickSfx;

    private void Awake()
    {
        if(Instance != null)
        {
            Logger.LogError("SoundManager Instance not null");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        CreateAudioSource(5);
    }

    private void CreateAudioSource(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateAudioSource();
        }
    }

    private AudioSource CreateAudioSource()
    {
        GameObject obj = new GameObject("AudioSource");
        obj.transform.SetParent(transform);

        AudioSource source = obj.AddComponent<AudioSource>();
        source.playOnAwake = false;

        sfxSourceList.Add(source);

        return source;
    }

    private AudioSource GetEmptySource()
    {
        for (int i = 0; i < sfxSourceList.Count; i++)
        {
            if(!sfxSourceList[i].isPlaying)
            {
                return sfxSourceList[i];
            }
        }

        return CreateAudioSource();
    }

    public void PlaySfx(AudioClip clip)
    {
        AudioSource source = GetEmptySource();

        source.clip = clip;
        source.Play();
    }
}
