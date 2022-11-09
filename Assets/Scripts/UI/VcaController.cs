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

    private float volume = 0f;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(SetVolume);

        vcaController = FMODUnity.RuntimeManager.GetVCA($"vca:/{vcaName}");
    }

    public void SetVolume(float volume)
    {
        vcaController.setVolume(volume);
    }

    public float GetVolume()
    {
        vcaController.getVolume(out volume);
        return volume;
    }

    public void SetSlider(float volume)
    {
        slider.value = volume;
    }

}
