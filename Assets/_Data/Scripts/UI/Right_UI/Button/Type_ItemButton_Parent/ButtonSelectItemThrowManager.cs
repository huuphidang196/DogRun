using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectItemThrowManager : StandMonoBehaviour
{
    [SerializeField] protected ItemType _ItemType;
    public ItemType ItemType => this._ItemType;

    [SerializeField] protected GameObject _Image_Light;
    [SerializeField] protected ButtonUIStandard _Button_Select;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemType();
        this.LoadButton();
        this.LoadImageLight();
    }
    protected virtual void LoadItemType()
    {
        if (this._ItemType != ItemType.None) return; // đã set tay trong Inspector, không ghi đè

        string typeName = this.gameObject.name
            .Replace("Button_", "")
            .Replace("_Manager", "");

        if (Enum.TryParse(typeName, out ItemType result))
        {
            this._ItemType = result;
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy ItemType khớp với '{typeName}' từ tên object '{this.name}'.");
        }
    }
    protected virtual void LoadButton()
    {
        if (this._Button_Select != null) return;
        this._Button_Select = GetComponentInChildren<ButtonUIStandard>();
    }

    protected virtual void LoadImageLight()
    {
        if (this._Image_Light != null) return;
        this._Image_Light = transform.Find("Image_Light")?.gameObject;
    }

    // 👉 Set trạng thái sáng/tắt, được gọi từ Parent, KHÔNG tự ý quyết định
    public virtual void SetSelected(bool isSelected)
    {
        if (this._Image_Light != null)
            this._Image_Light.SetActive(isSelected);
    }
}
