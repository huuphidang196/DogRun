using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShowCountItem : BaseText
{
    [SerializeField] protected ButtonSelectItemManager _ButtonManager; // lấy type từ đây, không khai báo field riêng

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButtonManager();
    }
    protected virtual void LoadButtonManager()
    {
        if (this._ButtonManager != null) return;
        this._ButtonManager = GetComponentInParent<ButtonSelectItemManager>();
    }

    protected virtual void FixedUpdate()
    {
        this.RefreshCount();
    }

    protected virtual void RefreshCount()
    {
        int amount = PlayerCtrl.Instance.PlayerInventory.GetAmount(this._ButtonManager.ItemType);
        this.SetText(amount); // dùng lại hàm có sẵn từ BaseText, không viết lại logic set text
    }
}
