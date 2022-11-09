using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : TabPopup
{
    [SerializeField]
    private VcaController bgmController;
    [SerializeField]
    private VcaController sfxController;


    protected override void Start()
    {
        base.Start();

        SaveData data = DataManager.Instance.SaveData;

        if (data != null)
        {
            bgmController.SetSlider(data.bgmVolume);
            sfxController.SetSlider(data.sfxVolume);
        }
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveData = new SaveData()
            .SetBgmVolume(bgmController.GetVolume())
            .SetSfxVolume(sfxController.GetVolume());
    }
}
