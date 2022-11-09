using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VcaController : MonoBehaviour
{
    private FMOD.Studio.VCA vcaController;

    private Slider slider;

    [SerializeField]
    private string vcaName;

    [SerializeField]
    private float vcaVolume;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(SetVolume);
    }

    private void Start()
    {
        vcaController = FMODUnity.RuntimeManager.GetVCA($"vca:/{vcaName}");
        vcaController.getVolume(out vcaVolume);
    }

    public void SetVolume(float volume)
    {
        vcaController.setVolume(volume);
        vcaVolume = volume;
    }
}
