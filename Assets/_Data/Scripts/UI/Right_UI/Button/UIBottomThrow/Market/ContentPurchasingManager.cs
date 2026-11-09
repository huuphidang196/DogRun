using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPurchasingManager : StandMonoBehaviour
{
    [Header("References")]
    [SerializeField] protected ItemExchangeTransaction _ItemExchangeTransaction;
    public ItemExchangeTransaction ItemExchangeTransaction => this._ItemExchangeTransaction;

    [SerializeField] protected ExchangePanelManager _ExchangePanelManager;

    [Header("Selection")]
    [SerializeField] protected ItemButtonExchangeManager _SelectedItemButton;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadItemExchangeTransaction();
        this.LoadExchangePanelManager();
    }

    protected virtual void LoadItemExchangeTransaction()
    {
        if (this._ItemExchangeTransaction != null) return;

        this._ItemExchangeTransaction =
            transform.GetComponentInChildren<ItemExchangeTransaction>(true);
    }

    protected virtual void LoadExchangePanelManager()
    {
        if (this._ExchangePanelManager != null) return;

        this._ExchangePanelManager =
            transform.GetComponentInChildren<ExchangePanelManager>(true);
    }

    public virtual void SelectItem(
     ItemButtonExchangeManager itemButton,
     ItemExchangeDataSO itemData,
     ExchangeMode mode)
    {
        if (itemButton == null || itemData == null) return;
        if (this._ExchangePanelManager == null) return;

        if (this._SelectedItemButton != itemButton)
        {
            if (this._SelectedItemButton != null)
                this._SelectedItemButton.SetSelected(false);

            this._SelectedItemButton = itemButton;
            this._SelectedItemButton.SetSelected(true);
        }

        this._ExchangePanelManager.Show(itemData, mode);
    }

    public virtual void ClearSelection()
    {
        if (this._SelectedItemButton != null)
            this._SelectedItemButton.SetSelected(false);

        this._SelectedItemButton = null;
    }

    public virtual void ClearSelection(ItemButtonExchangeManager itemButton)
    {
        if (this._SelectedItemButton != itemButton) return;

        this.ClearSelection();
    }

    public virtual void CloseShop()
    {
        this.ClearSelection();

        if (this._ExchangePanelManager != null)
            this._ExchangePanelManager.ResetPanel();

        gameObject.SetActive(false);
    }
}
