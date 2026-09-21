using System.Collections.Generic;
using UnityEngine;

public class UITypeItemButtonPlantParent : UITypeItemButtonParent
{
    protected override void OnEnable()
    {
        base.OnEnable();

        if (PlayerCtrl.Instance != null &&
            PlayerCtrl.Instance.PlayerInventory != null)
        {
            PlayerCtrl.Instance.PlayerInventory.OnItemAmountChanged
                += OnItemAmountChanged;
        }

        RefreshButtons();
    }

    protected override void OnDisable()
    {
        if (PlayerCtrl.Instance != null &&
            PlayerCtrl.Instance.PlayerInventory != null)
        {
            PlayerCtrl.Instance.PlayerInventory.OnItemAmountChanged
                -= OnItemAmountChanged;
        }

        base.OnDisable();
    }


    // ============================================
    // INVENTORY CHANGED
    // ============================================

    protected virtual void OnItemAmountChanged(ItemType type, int amount)
    {
        RefreshButtons();

        if (!HasItem(type))
            SelectNextItem(type);
    }


    // ============================================
    // SELECT
    // ============================================

    public override void SelectItem(ItemType type)
    {
        if (!HasItem(type))
        {
            SelectNextItem(type);
            return;
        }

        GameDataManager.Instance.SetCropPlantUsing(type);

        base.SelectItem(type);
    }


    // ============================================
    // REFRESH BUTTON
    // ============================================

    protected virtual void RefreshButtons()
    {
        if (this._Buttons == null || this._Buttons.Count == 0)
            return;

        foreach (ButtonSelectItemManager button in this._Buttons)
        {
            if (button == null)
                continue;

            button.gameObject.SetActive(
                HasItem(button.ItemType)
            );
        }
    }


    // ============================================
    // CHECK INVENTORY
    // ============================================

    protected virtual bool HasItem(ItemType type)
    {
        if (PlayerCtrl.Instance == null)
            return false;

        if (PlayerCtrl.Instance.PlayerInventory == null)
            return false;

        return PlayerCtrl.Instance.PlayerInventory.GetAmount(type) > 0;
    }


    // ============================================
    // SELECT NEXT ITEM
    // ============================================

    protected virtual void SelectNextItem(ItemType currentType)
    {
        if (this._Buttons == null || this._Buttons.Count == 0)
        {
            DisableAllButtons();
            return;
        }

        int currentIndex = GetButtonIndex(currentType);

        for (int i = 1; i <= this._Buttons.Count; i++)
        {
            int index = (currentIndex + i) % this._Buttons.Count;

            ButtonSelectItemManager button = this._Buttons[index];

            if (button == null)
                continue;

            if (!HasItem(button.ItemType))
                continue;

            SelectItem(button.ItemType);
            return;
        }

        // Không còn item nào
        DisableAllButtons();
    }


    // ============================================
    // GET INDEX
    // ============================================

    protected virtual int GetButtonIndex(ItemType type)
    {
        if (this._Buttons == null)
            return -1;

        for (int i = 0; i < this._Buttons.Count; i++)
        {
            if (this._Buttons[i] == null)
                continue;

            if (this._Buttons[i].ItemType == type)
                return i;
        }

        return -1;
    }


    // ============================================
    // DISABLE ALL
    // ============================================

    protected virtual void DisableAllButtons()
    {
        if (this._Buttons == null)
            return;

        foreach (ButtonSelectItemManager button in this._Buttons)
        {
            if (button == null)
                continue;

            button.gameObject.SetActive(false);
        }
    }
}