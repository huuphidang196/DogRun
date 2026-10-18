using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnSwapItemThrow : btnSelectItemThrow
{
    [SerializeField] protected ButtonSwapManager _ButtonSwapManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadButtonSwapManager();
    }

    protected virtual void LoadButtonSwapManager()
    {
        if (this._ButtonSwapManager != null) return;

        this._ButtonSwapManager = GetComponentInParent<ButtonSwapManager>();
    }

    protected override void OnClick()
    {
        if (this._Parent == null || this._ButtonManager == null) return;
        this._Parent.SelectItem(this._ButtonSwapManager.ItemType);

        // Debug.Log(this._ButtonManager.ItemType.ToString());
    }
}
