using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShowCountItem : BaseText
{
    [SerializeField] protected ItemType _ItemType;
    public ItemType ItemType => this._ItemType;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemType();
    }
    protected virtual void LoadItemType()
    {
        if (this._ItemType != ItemType.None) return;

        string objectName = this.transform.parent.name;

        int lastIndex = objectName.LastIndexOf('_');

        if (lastIndex < 0) return;

        string typeName = objectName.Substring(lastIndex + 1);

        if (Enum.TryParse(typeName, out ItemType result))
        {
            this._ItemType = result;
            return;
        }

        Debug.LogWarning(
            $"Không tìm thấy ItemType '{typeName}' từ GameObject '{objectName}'.",
            this
        );
    }

    protected virtual void FixedUpdate()
    {
        this.RefreshCount();
    }

    protected virtual void RefreshCount()
    {
        int amount = PlayerCtrl.Instance.PlayerInventory.GetAmount(this._ItemType);
        this.SetText(amount); // dùng lại hàm có sẵn từ BaseText, không viết lại logic set text
    }
}
