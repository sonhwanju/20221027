using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopup : TabPopup
{
    [SerializeField]
    private AutoScroll autoScroll;

    public override void Open()
    {
        base.Open();

        autoScroll.StartScroll();
    }

    public override void Close()
    {
        autoScroll.StopScroll();

        base.Close();
    }
}
