using UnityEngine;

public class ItemExchangeTransaction : StandMonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventory _PlayerInventory;
    [SerializeField] private PlayerWallet _PlayerWallet;

    public virtual bool TryExchange(
        ItemExchangeDataSO itemData,
        ExchangeMode mode,
        int amount)
    {
        if (itemData == null || amount <= 0)
            return false;

        if (_PlayerInventory == null || _PlayerWallet == null)
            return false;

        // Kiểm tra Item có tồn tại trong Inventory.
        if (_PlayerInventory.GetItemDateByType(itemData.ItemType) == null)
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
        if (!_PlayerWallet.HasEnoughMoney(totalPrice))
            return false;

        // Trừ tiền.
        if (!_PlayerWallet.SpendMoney(totalPrice))
            return false;

        // Cộng vật phẩm.
        _PlayerInventory.AddItem(itemData.ItemType, amount);

        return true;
    }

    protected virtual bool SellItem(
        ItemExchangeDataSO itemData,
        int amount,
        int totalPrice)
    {
        // Kiểm tra số lượng vật phẩm.
        if (_PlayerInventory.GetAmount(itemData.ItemType) < amount)
            return false;

        // Trừ vật phẩm.
        if (!_PlayerInventory.RemoveItem(itemData.ItemType, amount))
            return false;

        // Cộng tiền.
        _PlayerWallet.AddMoney(totalPrice);

        return true;
    }
}