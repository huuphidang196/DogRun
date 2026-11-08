using UnityEngine;

[CreateAssetMenu(
    fileName = "ItemExchangeData",
    menuName = "SO/Market/Item Exchange Data"
)]
public class ItemExchangeDataSO : ScriptableObject
{
    [Header("Item")]
    [SerializeField] private ItemType _ItemType;
    [SerializeField] private Sprite _Icon;
    [SerializeField] private string _ItemName;

    [Header("Price")]
    [SerializeField] private int _BuyPrice;
    [SerializeField] private int _SellPrice;

    [Header("Trade Settings")]
    [SerializeField] private bool _CanBuy = true;
    [SerializeField] private bool _CanSell = true;

    public ItemType ItemType => _ItemType;
    public Sprite Icon => _Icon;
    public string ItemName => _ItemName;

    public int BuyPrice => _BuyPrice;
    public int SellPrice => _SellPrice;

    public bool CanBuy => _CanBuy;
    public bool CanSell => _CanSell;
}
public enum ExchangeMode
{
    Buy,
    Sell
}