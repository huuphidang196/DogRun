using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnSelectItemThrow : ButtonUIStandard
{
    [SerializeField] protected UITypeItemButtonParent _Parent;
    [SerializeField] protected ButtonSelectItemThrowManager _ButtonManager; // lấy type từ đây, không khai báo field riêng

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadParent();
        this.LoadButtonManager();
    }

    protected virtual void LoadParent()
    {
        if (this._Parent != null) return;
        this._Parent = GetComponentInParent<UITypeItemButtonParent>();
    }

    protected virtual void LoadButtonManager()
    {
        if (this._ButtonManager != null) return;
        this._ButtonManager = GetComponentInParent<ButtonSelectItemThrowManager>();
    }

    protected override void OnClick()
    {
        if (this._Parent == null || this._ButtonManager == null) return;
        this._Parent.SelectItem(this._ButtonManager.ItemType);
    }
}
