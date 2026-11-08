using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemExchangeManager : StandMonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ExchangeMode _ExchangeMode;

    [Header("References")]
    [SerializeField] private ExchangePanelManager _ExchangePanel;

    private List<ItemButtonExchangeManager> _ItemButtons = new();

    private List<ItemExchangeDataSO> _ItemExchangeSO;

    private ItemButtonExchangeManager _SelectedItem;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListDataExchangeSO();
    }

    private void LoadListDataExchangeSO()
    {

        if (this._ItemExchangeSO.Count > 0) return;

        _ItemExchangeSO.Clear();

        ItemExchangeDataSO[] itemSOs =
            Resources.LoadAll<ItemExchangeDataSO>("SO/Market/ItemExchange");

        _ItemExchangeSO.AddRange(itemSOs);
    }


    protected override void Start()
    {
        base.Start();
        LoadItemButtons();
    }

    protected virtual void LoadItemButtons()
    {
        _ItemButtons.Clear();


        _ItemButtons.AddRange(
            GetComponentsInChildren<ItemButtonExchangeManager>(true)
        );

        foreach (ItemButtonExchangeManager item in _ItemButtons)
        {
            if (item == null) continue;

            item.SetManager(this);
            item.SetSelected(false);
        }
    }

    public virtual void SelectItem(ItemButtonExchangeManager item)
    {
        if (item == null || item.ItemData == null) return;

        ItemExchangeDataSO data = item.ItemData;

        if (_ExchangeMode == ExchangeMode.Buy && !data.CanBuy)
            return;

        if (_ExchangeMode == ExchangeMode.Sell && !data.CanSell)
            return;

        if (_SelectedItem != null)
            _SelectedItem.SetSelected(false);

        _SelectedItem = item;
        _SelectedItem.SetSelected(true);

        _ExchangePanel.Show(data, _ExchangeMode);
    }

    public virtual void ClearSelection()
    {
        if (_SelectedItem != null)
            _SelectedItem.SetSelected(false);

        _SelectedItem = null;
    }
}