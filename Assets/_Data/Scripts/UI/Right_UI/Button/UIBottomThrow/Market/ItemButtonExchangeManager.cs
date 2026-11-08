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

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadImageLight();
        this.LoadImageItem();
        this.LoadTextNameItem();
        this.LoadButtonExchange();           
    }

    protected virtual void LoadButtonExchange()
    {
        if (_ButtonExchange != null) return;

        Transform buttonTransform = transform.Find("Button_Exchange");

        if (buttonTransform != null)
            _ButtonExchange = buttonTransform.GetComponentInChildren<Button>(true);
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

    public virtual void SetManager(ItemExchangeManager manager)
    {
        _ExchangeManager = manager;
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
        if (_ExchangeManager == null) return;

        _ExchangeManager.SelectItem(this);
    }

    public virtual void SetSelected(bool selected)
    {
        if (_ImageLight != null)
            _ImageLight.SetActive(selected);
    }

    public virtual void SetItemExchangeSO(ItemExchangeDataSO so) => this._ItemDataSO = so;
}