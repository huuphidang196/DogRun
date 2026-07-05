using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowButton : ButtonUIStandard
{
    protected override void OnClick()
    {
        InputManagerInstance.Instance.SetPlayerThrowInput(true);
    }
}
