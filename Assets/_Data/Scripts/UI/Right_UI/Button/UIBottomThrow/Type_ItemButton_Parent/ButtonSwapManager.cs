using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwapManager : ButtonSelectItemManager
{
    [SerializeField] protected UITypeItemButtonThrowParent _Parent;

    [SerializeField] protected Image _Image_WantChanging;
    //[SerializeField] protected bool allowChange = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
        this.LoadParent();
        this.LoadImageWantChanging();
    }

    protected virtual void LoadImageWantChanging()
    {
        if (this._Image_WantChanging != null) return;
        this._Image_WantChanging = transform.Find("Image_Rep_Item_Swicth").GetComponentInChildren<Image>();
    }
    protected virtual void LoadParent()
    {
        if (this._Parent != null) return;
        this._Parent = GetComponentInParent<UITypeItemButtonThrowParent>();
    }

    protected override void LoadImageLight()
    {
        //null
    }

    protected override void LoadItemType()
    {
        this._ItemType = ItemType.Banana;
    }

    public override void SetSelected(ItemType typeSelected)
    {
        //if select slipper swap change to banana. ItemType of Manager => want change and parent is current Type for using
        this._ItemType = typeSelected == ItemType.Slipper ? ItemType.Banana : ItemType.Slipper;

        //if type selected does has
        if (this._Image_WantChanging == null) return;

        this._Image_WantChanging.sprite = this._Parent.GetItemSpriteByType(this._ItemType);
    }

    protected virtual void Update()
    {
        this.ProcessSwapAutomation();
        this.ProcessActiveUISwap();
    }

    protected virtual void ProcessActiveUISwap()
    {
        if (PlayerCtrl.Instance.PlayerInventory.HasItem(this._ItemType) == this._Image_WantChanging.gameObject.activeInHierarchy) return;

        this._Button_Select.BtnExect.interactable = !this._Image_WantChanging.gameObject.activeInHierarchy;
        this._Image_WantChanging.enabled = !this._Image_WantChanging.gameObject.activeInHierarchy;
    }

    protected virtual void ProcessSwapAutomation()
    {
        if (this._Parent.CurrentItem_Selected == ItemType.None) return;

        // Check count Current using
        if (PlayerCtrl.Instance.PlayerInventory.HasItem(this._Parent.CurrentItem_Selected)) return;

        this._Parent.SelectItem(this._ItemType);

    }
}
