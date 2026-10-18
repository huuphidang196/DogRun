using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnFishingMode : ButtonUIStandard
{
    protected override void OnClick()
    {
        AreaManager.Instance.LakeFishingManager.SetAutoFishing();
    }
}
