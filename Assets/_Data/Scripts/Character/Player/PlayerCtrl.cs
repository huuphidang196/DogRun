using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : ObjCtrl
{
    public static PlayerCtrl Instance;

    protected override void Awake()
    {
        base.Awake();
        
        if (Instance != null) return;

        Instance = this;
    }
 
}
