using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManagerAbstract : PlayerCtrlAbstract
{
    [SerializeField] protected PlayerActionManager _PlayerActionManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadPlayerActionManager();
    }

    protected virtual void LoadPlayerActionManager()
    {
        if (this._PlayerActionManager != null) return;

        this._PlayerActionManager = GetComponentInParent<PlayerActionManager>();
    }
}
