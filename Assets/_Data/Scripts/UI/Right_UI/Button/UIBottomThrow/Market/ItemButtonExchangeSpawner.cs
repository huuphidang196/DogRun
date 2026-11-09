using UnityEngine;

public class ItemButtonExchangeSpawner : Spawner<ItemButtonExchangeSpawner>
{
    [Header("Exchange Holders")]
    [SerializeField] protected Transform _SellHolder;
    [SerializeField] protected Transform _BuyHolder;

    protected override void LoadHolder()
    {
        this.LoadSellHolder();
        this.LoadBuyHolder();
    }

    protected virtual void LoadSellHolder()
    {
        if (this._SellHolder != null) return;

        this._SellHolder = transform.parent.Find(
            "Sell_Panel_Manager/Scroll_View_Sell_Panel/Viewport/Content"
        );
    }

    protected virtual void LoadBuyHolder()
    {
        if (this._BuyHolder != null) return;

        this._BuyHolder = transform.parent.Find(
            "Buy_Panel_Manager/Scroll_View_Buy_Panel/Viewport/Content"
        );
    }

    public virtual Transform GetHolder(ExchangeMode mode)
    {
        return mode == ExchangeMode.Buy
            ? this._BuyHolder
            : this._SellHolder;
    }

    public virtual ItemButtonExchangeManager SpawnItem(
        ItemExchangeDataSO itemData,
        ExchangeMode mode,
        ItemExchangeManager manager)
    {
        if (itemData == null) return null;

        Transform holder = this.GetHolder(mode);
        if (holder == null) return null;

        Transform obj = this.SpawnObject(
            "Item_Button_Exchange_Manager",
            holder
        );

        if (obj == null) return null;

        ItemButtonExchangeManager item =
            obj.GetComponent<ItemButtonExchangeManager>();

        if (item == null)
        {
            this.DespawnObject(obj);
            return null;
        }

        item.SetItemData(itemData);
        item.SetManager(manager);
        item.SetSelected(false);

        return item;
    }

    public virtual void DespawnItems(Transform holder)
    {
        if (holder == null) return;

        for (int i = holder.childCount - 1; i >= 0; i--)
        {
            Transform obj = holder.GetChild(i);

            ItemButtonExchangeManager item =
                obj.GetComponent<ItemButtonExchangeManager>();

            if (item != null)
                item.ResetItem();

            this.DespawnObject(obj);
        }
    }
}