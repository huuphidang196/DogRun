using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBottomThrowManager : StandMonoBehaviour
{
    [SerializeField] protected UITypeItemButtonThrowParent _UITypeItemButtonThrowParent;
    public UITypeItemButtonThrowParent UITypeItemButtonThrowParent => _UITypeItemButtonThrowParent;

    [SerializeField] protected ThrowButtonUIActiveParent _ThrowButtonUIActiveParent;
    public ThrowButtonUIActiveParent ThrowButtonUIActiveParent => _ThrowButtonUIActiveParent;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadUITypeItemButtonThrowParent();
        this.LoadThrowButtonUIActiveParent();
    }

    protected virtual void LoadThrowButtonUIActiveParent()
    {
        if (this._ThrowButtonUIActiveParent != null) return;

        this._ThrowButtonUIActiveParent = GetComponentInChildren<ThrowButtonUIActiveParent>();
    }

    protected virtual void LoadUITypeItemButtonThrowParent()
    {
        if (this._UITypeItemButtonThrowParent != null) return;

        this._UITypeItemButtonThrowParent = GetComponentInChildren<UITypeItemButtonThrowParent>();
    }

}
