using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemExchangeManager : ContentPurchasingManagerAbstract
{
    [Header("Settings")]
    [SerializeField] private ExchangeMode _ExchangeMode;

    [Header("Item Data")]
    [SerializeField] protected List<ItemExchangeDataSO> _ItemDatas_SO;

    private List<ItemButtonExchangeManager> _ItemButtons = new();

    protected bool _isSpawned;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListDataExchangeSO();
    }

    private void LoadListDataExchangeSO()
    {

        if (this._ItemDatas_SO.Count > 0) return;

        _ItemDatas_SO.Clear();

        ItemExchangeDataSO[] itemSOs =
            Resources.LoadAll<ItemExchangeDataSO>("SO/Market/ItemExchange");

        _ItemDatas_SO.AddRange(itemSOs);
    }


    protected override void Start()
    {
        base.Start();
        this.SpawnItems();
    }

    public virtual void SpawnItems()
    {
        if (this._isSpawned) return;

        ItemButtonExchangeSpawner spawner = ItemButtonExchangeSpawner.Instance;

        if (spawner == null)
        {
            Debug.LogWarning("ItemButtonExchangeSpawner chưa được khởi tạo.");
            return;
        }

        this._ItemButtons.Clear();

        foreach (ItemExchangeDataSO itemData in this._ItemDatas_SO)
        {
            if (itemData == null) continue;

            bool canExchange = this._ExchangeMode == ExchangeMode.Buy ? itemData.CanBuy : itemData.CanSell;

            if (!canExchange) continue;

            ItemButtonExchangeManager item = spawner.SpawnItem(
                itemData,
                this._ExchangeMode,
                this
            );

            if (item == null) continue;

            this._ItemButtons.Add(item);
        }

        this._isSpawned = true;
    }
    public virtual void SelectItem(ItemButtonExchangeManager item)
    {
        if (item == null || item.ItemData == null) return;
        if (this._ContentPurchasingManager == null) return;

        this._ContentPurchasingManager.SelectItem(
            item,
            item.ItemData,
            this._ExchangeMode
        );
    }

    public virtual void DespawnItems()
    {
        if (this._ItemButtons.Count == 0) return;

        ItemButtonExchangeSpawner spawner =
            ItemButtonExchangeSpawner.Instance;

        if (spawner == null) return;

        // Chỉ xóa lựa chọn nếu item đang chọn thuộc danh sách này.
        if (this._ContentPurchasingManager != null)
        {
            foreach (ItemButtonExchangeManager item in this._ItemButtons)
            {
                this._ContentPurchasingManager.ClearSelection(item);
            }
        }

        spawner.DespawnItems(
            spawner.GetHolder(this._ExchangeMode)
        );

        this._ItemButtons.Clear();
        this._isSpawned = false;
    }

}