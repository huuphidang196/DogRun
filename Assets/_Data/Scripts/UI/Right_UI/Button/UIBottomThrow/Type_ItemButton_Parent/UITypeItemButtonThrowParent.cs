using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITypeItemButtonThrowParent : UITypeItemButtonParent
{
    [SerializeField] protected UIBottomThrowManager _UIBottomThrowManager;
    [SerializeField] protected List<Sprite> _Sprites_Throw_Item;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadUIBottomThrowManager();
    }

    protected virtual void LoadUIBottomThrowManager()
    {
        if (this._UIBottomThrowManager != null) return;

        this._UIBottomThrowManager = GetComponentInParent<UIBottomThrowManager>();
    }

    // 👉 Được gọi từ UnityEvent OnClick của mỗi button trong Inspector
    public override void SelectItem(ItemType type)
    {
        if (type == ItemType.Fish) return;

        if (!PlayerCtrl.Instance.PlayerInventory.HasItem(type)) return;
     
        this._CurrentSelected = type;
     
        // 1. Báo cho Spawner biết loại item đang chọn
        PlayerWeaponSpawner.Instance.SelectThrowItem(type);

        //Notify for Button Throw changeRep
        this._UIBottomThrowManager.ThrowButtonUIActiveParent.ChangeItemToThrow(type, this.GetItemSpriteByType(type));

        base.SelectItem(type);
    }
    public virtual Sprite GetItemSpriteByType(ItemType type)
    {
        if (type == ItemType.None || type == ItemType.Fish) return null;

        if (type == ItemType.Slipper) return this._Sprites_Throw_Item[0];

        return this._Sprites_Throw_Item[1];
    }
}
