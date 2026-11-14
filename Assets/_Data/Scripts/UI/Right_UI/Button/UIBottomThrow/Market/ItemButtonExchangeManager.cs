
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

    [SerializeField] private Button _ButtonExchangeBG;
    [SerializeField] private Button _ButtonExchange;
    [SerializeField] private Image _ImageExchangeButton;

    [Header("Exchange Sprites")]
    [SerializeField] private Sprite _SpriteBuy;
    [SerializeField] private Sprite _SpriteSell;

    private ItemExchangeManager _ExchangeManager;

    public ItemExchangeDataSO ItemData => _ItemDataSO;

    protected override void Awake()
    {
        base.Awake();

        this.RefreshUI();

        if (_ButtonExchangeBG != null)
            _ButtonExchangeBG.onClick.AddListener(OnButtonClick);

        if (_ButtonExchange != null)
            _ButtonExchange.onClick.AddListener(OnButtonClick);
    }

    protected virtual void OnDestroy()
    {
        if (_ButtonExchangeBG != null)
            _ButtonExchangeBG.onClick.RemoveListener(OnButtonClick);

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
        this.LoadButtonExchangeBG();
        this.LoadButtonExchange();
        this.LoadTextExchange();
        this.LoadImageExchangeButton();
    }

    protected virtual void LoadButtonExchangeBG()
    {
        if (_ButtonExchangeBG != null) return;

        Transform buttonTransform = transform.Find("Button_Exchange_BG");

        if (buttonTransform != null)
            _ButtonExchangeBG = buttonTransform.GetComponent<Button>();
    }

    protected virtual void LoadButtonExchange()
    {
        if (_ButtonExchange != null) return;

        Transform buttonTransform = transform.Find("Button_Exchange");

        if (buttonTransform != null)
            _ButtonExchange = buttonTransform.GetComponent<Button>();
    }

    protected virtual void LoadTextExchange()
    {
        if (_TextExchange != null) return;

        Transform textTransform = transform.Find(
            "Button_Exchange/Content_Exchange"
        );

        if (textTransform != null)
            _TextExchange = textTransform.GetComponent<TMP_Text>();
    }

    protected virtual void LoadImageExchangeButton()
    {
        if (_ImageExchangeButton != null) return;

        if (_ButtonExchange != null)
            _ImageExchangeButton = _ButtonExchange.GetComponent<Image>();
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
        this.RefreshExchangeUI();
    }

    protected virtual void RefreshExchangeUI()
    {
        if (this._ExchangeManager == null) return;

        bool isBuy = this._ExchangeManager.ExchangeMode == ExchangeMode.Buy;

        if (_TextExchange != null)
            _TextExchange.text = isBuy ? "MUA" : "BÁN";

        if (_ImageExchangeButton != null)
        {
            _ImageExchangeButton.sprite = isBuy
                ? _SpriteBuy
                : _SpriteSell;
        }
    }

    protected virtual void RefreshUI()
    {
        if (_ItemDataSO == null) return;

        if (_ImageItem != null)
            _ImageItem.sprite = _ItemDataSO.Icon;

        if (_TextNameItem != null)
            _TextNameItem.text = _ItemDataSO.ItemName_Vie;
    }

    protected virtual void OnButtonClick()
    {
        if (_ExchangeManager == null) return;
        if (_ItemDataSO == null) return;

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

        if (_ImageItem != null)
            _ImageItem.sprite = null;

        if (_TextNameItem != null)
            _TextNameItem.text = string.Empty;

        if (_TextExchange != null)
            _TextExchange.text = string.Empty;

        if (_ImageExchangeButton != null)
            _ImageExchangeButton.sprite = null;
    }
}
