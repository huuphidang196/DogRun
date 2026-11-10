using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonExchangeManager : StandMonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ItemExchangeDataSO _ItemDataSO;

    [Header("UI References")]
    [SerializeField] private GameObject _ImageLight;
    [SerializeField] private Image _ImageItem;
    [SerializeField] private TMP_Text _TextNameItem;
    [SerializeField] private TMP_Text _TextExchange;
    [SerializeField] private Button _ButtonExchange;

    private ItemExchangeManager _ExchangeManager;

    public ItemExchangeDataSO ItemData => _ItemDataSO;

    protected override void Awake()
    {
        base.Awake();
        RefreshUI();

        if (_ButtonExchange != null)
            _ButtonExchange.onClick.AddListener(OnButtonClick);
    }

    protected virtual void OnDestroy()
    {
        if (_ButtonExchange != null)
            _ButtonExchange.onClick.RemoveListener(OnButtonClick);
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadImageLight();
        this.LoadImageItem();
        this.LoadTextNameItem();
        this.LoadTextExchange();
        this.LoadButtonExchange();
    }

    protected virtual void LoadButtonExchange()
    {
        if (_ButtonExchange != null) return;

        Transform buttonTransform = transform.Find("Button_Exchange");

        if (buttonTransform != null)
            _ButtonExchange = buttonTransform.GetComponentInChildren<Button>(true);
    }
    protected virtual void LoadTextExchange()
    {
        if (this._TextExchange != null) return;

        Transform textTransform = transform.Find(
            "Button_Exchange/Content_Exchange"
        );

        if (textTransform != null)
            this._TextExchange = textTransform.GetComponent<TMP_Text>();
    }
    protected virtual void LoadTextNameItem()
    {
        if (_TextNameItem != null) return;
        _TextNameItem = transform.Find("Text_Name_Item")?.GetComponent<TMP_Text>();
    }

    protected virtual void LoadImageItem()
    {
        if (_ImageItem != null) return;
        _ImageItem = transform.Find("Image_Item_Rep_Dynamic")?.GetComponent<Image>();
    }

    protected virtual void LoadImageLight()
    {
        if (_ImageLight != null) return;
        _ImageLight = transform.Find("Image_Light")?.gameObject;
    }
    #endregion LoadComponents

    public virtual void SetManager(ItemExchangeManager manager)
    {
        this._ExchangeManager = manager;
        this.RefreshExchangeText();
    }
    protected virtual void RefreshExchangeText()
    {
        if (this._TextExchange == null) return;
        if (this._ExchangeManager == null) return;

        this._TextExchange.text =
            this._ExchangeManager.ExchangeMode == ExchangeMode.Buy
            ? "MUA"
            : "BÁN";
    }
    protected virtual void RefreshUI()
    {
        if (_ItemDataSO == null) return;

        if (_ImageItem != null)
            _ImageItem.sprite = _ItemDataSO.Icon;

        if (_TextNameItem != null)
            _TextNameItem.text = _ItemDataSO.ItemName;
    }

    protected virtual void OnButtonClick()
    {
        if (this._ExchangeManager == null) return;

        this._ExchangeManager.SelectItem(this);
    }

    public virtual void SetSelected(bool selected)
    {
        if (_ImageLight != null)
            _ImageLight.SetActive(selected);
    }
    public virtual void SetItemData(ItemExchangeDataSO itemData)
    {
        if (itemData == null) return;

        this._ItemDataSO = itemData;
        this.RefreshUI();
    }

    public virtual void ResetItem()
    {
        this._ItemDataSO = null;
        this._ExchangeManager = null;

        this.SetSelected(false);

        if (this._ImageItem != null)
            this._ImageItem.sprite = null;

        if (this._TextNameItem != null)
            this._TextNameItem.text = string.Empty;
    }

}