using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenPopup : Popup
{
    [SerializeField]
    protected Button exitBtn;
    public Button ExitBtn => exitBtn;
}
