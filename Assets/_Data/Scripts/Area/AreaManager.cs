using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : Singleton<AreaManager>
{
    [SerializeField] protected LakeFishingManager _LakeFishingManager;

    public LakeFishingManager LakeFishingManager => _LakeFishingManager;

    
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadLakeFishingManager();
    }

    protected virtual void LoadLakeFishingManager()
    {
        if (this._LakeFishingManager != null) return;

        this._LakeFishingManager = GetComponentInChildren<LakeFishingManager>();
    }
}
