using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExchangePanelManager : ContentPurchasingManagerAbstract
{
    [Header("Panel")]
    [SerializeField] private GameObject _Panel;

    [Header("Item Information")]
    [SerializeField] private Image _ItemIcon;
    [SerializeField] private TMP_Text _ItemName;
    [SerializeField] private TMP_Text _UnitPrice;
    [SerializeField] private TMP_Text _AmountText;
    [SerializeField] private TMP_Text _TotalPrice;

    [Header("Buttons")]
    [SerializeField] private Button _ButtonPlus;
    [SerializeField] private Button _ButtonMinus;
    [SerializeField] private Button _ButtonConfirm;
    [SerializeField] private Button _ButtonClose;

    private ItemExchangeDataSO _CurrentItem;
    private ExchangeMode _CurrentMode;
    private int _Amount = 1;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_ButtonPlus != null)
            _ButtonPlus.onClick.AddListener(AddAmount);

        if (_ButtonMinus != null)
            _ButtonMinus.onClick.AddListener(SubtractAmount);

        if (_ButtonConfirm != null)
            _ButtonConfirm.onClick.AddListener(ConfirmExchange);

        if (_ButtonClose != null)
            _ButtonClose.onClick.AddListener(ClosePanel);
    }

    protected override void OnDisable()
    {
        if (_ButtonPlus != null)
            _ButtonPlus.onClick.RemoveListener(AddAmount);

        if (_ButtonMinus != null)
            _ButtonMinus.onClick.RemoveListener(SubtractAmount);

        if (_ButtonConfirm != null)
            _ButtonConfirm.onClick.RemoveListener(ConfirmExchange);

        if (_ButtonClose != null)
            _ButtonClose.onClick.RemoveListener(ClosePanel);

        base.OnDisable();
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadPanelExchange();
        this.LoadItemIcon();
        this.LoadItemName();
        this.LoadUnitPrice();
        this.LoadAmountText();
        this.LoadTotalPrice();

        this.LoadButtonPlus();
        this.LoadButtonMinus();
        this.LoadButtonConfirm();
        this.LoadButtonClose();
        this.LoadContentPurchasingManager();
    }

    protected virtual void LoadPanelExchange()
    {
        if (this._Panel != null) return;

        this._Panel = transform.Find("Exchange_Panel").gameObject;
    }

    protected virtual void LoadItemIcon()
    {
        if (this._ItemIcon != null) return;

        this._ItemIcon = transform.Find("Exchange_Panel/Item_Information/Image_Item")
            .GetComponent<Image>();
    }

    protected virtual void LoadItemName()
    {
        if (this._ItemName != null) return;

        this._ItemName = transform.Find("Exchange_Panel/Item_Information/Text_Item_Name")
            .GetComponent<TMP_Text>();
    }

    protected virtual void LoadUnitPrice()
    {
        if (this._UnitPrice != null) return;

        this._UnitPrice = transform.Find("Exchange_Panel/Exchange_Information/Text_Unit_Price")
            .GetComponent<TMP_Text>();
    }

    protected virtual void LoadAmountText()
    {
        if (this._AmountText != null) return;

        this._AmountText = transform.Find("Exchange_Panel/Exchange_Information/Text_Amount")
            .GetComponent<TMP_Text>();
    }

    protected virtual void LoadTotalPrice()
    {
        if (this._TotalPrice != null) return;

        this._TotalPrice = transform.Find("Exchange_Panel/Exchange_Information/Text_Total_Price")
            .GetComponent<TMP_Text>();
    }

    protected virtual void LoadButtonPlus()
    {
        if (this._ButtonPlus != null) return;

        this._ButtonPlus = transform.Find("Exchange_Panel/Exchange_Information/Button_Plus")
            .GetComponent<Button>();
    }

    protected virtual void LoadButtonMinus()
    {
        if (this._ButtonMinus != null) return;

        this._ButtonMinus = transform.Find("Exchange_Panel/Exchange_Information/Button_Minus")
            .GetComponent<Button>();
    }

    protected virtual void LoadButtonConfirm()
    {
        if (this._ButtonConfirm != null) return;

        this._ButtonConfirm = transform.Find("Exchange_Panel/Button_Confirm")
            .GetComponent<Button>();
    }

    protected virtual void LoadButtonClose()
    {
        if (this._ButtonClose != null) return;

        this._ButtonClose = transform.Find("Exchange_Panel/Header/Button_Close")
            .GetComponent<Button>();
    }

    #endregion LoadComponents

    public virtual void Show(ItemExchangeDataSO itemData, ExchangeMode mode)
    {
        if (itemData == null) return;

        _CurrentItem = itemData;
        _CurrentMode = mode;
        _Amount = 1;

        RefreshUI();

        _Panel.SetActive(true);
    }

    // Chỉ đóng giao diện, giữ nguyên CurrentItem và Selected Button.
    public virtual void ClosePanel()
    {
        if (this._Panel != null)
            this._Panel.SetActive(false);
    }

    public virtual void ResetPanel()
    {
        this.ClosePanel();

        this._CurrentItem = null;
        this._CurrentMode = ExchangeMode.Buy;
        this._Amount = 1;
    }

    protected virtual void AddAmount()
    {
        if (_Amount == int.MaxValue) return;

        _Amount++;
        RefreshUI();
    }

    protected virtual void SubtractAmount()
    {
        if (_Amount <= 1) return;

        _Amount--;
        RefreshUI();
    }

    protected virtual void RefreshUI()
    {
        if (_CurrentItem == null) return;

        int unitPrice = GetUnitPrice();

        _ItemIcon.sprite = _CurrentItem.Icon;
        _ItemName.text = _CurrentItem.ItemName;

        _UnitPrice.text = unitPrice.ToString();
        _AmountText.text = _Amount.ToString();

        long totalPrice = (long)unitPrice * _Amount;

        _TotalPrice.text = totalPrice.ToString();

        RefreshButtonState();
    }

    protected virtual int GetUnitPrice()
    {
        return _CurrentMode == ExchangeMode.Buy
            ? _CurrentItem.BuyPrice
            : _CurrentItem.SellPrice;
    }

    protected virtual void RefreshButtonState()
    {
        if (_ButtonConfirm == null || _CurrentItem == null)
            return;

        int unitPrice = GetUnitPrice();

        long totalPrice = (long)unitPrice * _Amount;

        bool valid = unitPrice > 0 &&
                     totalPrice <= int.MaxValue;

        _ButtonConfirm.interactable = valid;
    }

    protected virtual void ConfirmExchange()
    {
        if (_CurrentItem == null ||
            this._ContentPurchasingManager.ItemExchangeTransaction == null)
            return;

        bool success = this._ContentPurchasingManager.ItemExchangeTransaction.TryExchange(
            _CurrentItem,
            _CurrentMode,
            _Amount
        );

        if (!success)
        {
            Debug.Log("Giao dịch thất bại!");
            return;
        }

        Debug.Log("Giao dịch thành công!");

        this.ClosePanel();
    }
}