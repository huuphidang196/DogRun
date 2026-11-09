using UnityEngine;

public class ItemExchangeTransaction : ContentPurchasingManagerAbstract
{
    public virtual bool TryExchange(
    ItemExchangeDataSO itemData,
    ExchangeMode mode,
    int amount)
    {
        if (itemData == null || amount <= 0)
            return false;

        // Kiểm tra quyền mua/bán của vật phẩm.
        if (mode == ExchangeMode.Buy && !itemData.CanBuy)
            return false;

        if (mode == ExchangeMode.Sell && !itemData.CanSell)
            return false;

        if (PlayerCtrl.Instance.PlayerInventory == null || PlayerCtrl.Instance.PlayerWallet == null)
            return false;

        // Kiểm tra Item có tồn tại trong Inventory.
        if (PlayerCtrl.Instance.PlayerInventory.GetItemDateByType(itemData.ItemType) == null)
            return false;

        int unitPrice = mode == ExchangeMode.Buy
            ? itemData.BuyPrice
            : itemData.SellPrice;

        if (unitPrice <= 0 || amount > int.MaxValue / unitPrice)
            return false;

        int totalPrice = unitPrice * amount;

        if (mode == ExchangeMode.Buy)
            return BuyItem(itemData, amount, totalPrice);

        return SellItem(itemData, amount, totalPrice);
    }

    protected virtual bool BuyItem(
        ItemExchangeDataSO itemData,
        int amount,
        int totalPrice)
    {
        // Kiểm tra đủ tiền trước.
        if (!PlayerCtrl.Instance.PlayerWallet.HasEnoughMoney(totalPrice))
            return false;

        // Trừ tiền.
        if (!PlayerCtrl.Instance.PlayerWallet.SpendMoney(totalPrice))
            return false;

        // Cộng vật phẩm.
        PlayerCtrl.Instance.PlayerInventory.AddItem(itemData.ItemType, amount);

        return true;
    }

    protected virtual bool SellItem(
        ItemExchangeDataSO itemData,
        int amount,
        int totalPrice)
    {
        // Kiểm tra số lượng vật phẩm.
        if (PlayerCtrl.Instance.PlayerInventory.GetAmount(itemData.ItemType) < amount)
            return false;

        // Trừ vật phẩm.
        if (!PlayerCtrl.Instance.PlayerInventory.RemoveItem(itemData.ItemType, amount))
            return false;

        // Cộng tiền.
        PlayerCtrl.Instance.PlayerWallet.AddMoney(totalPrice);

        return true;
    }
}