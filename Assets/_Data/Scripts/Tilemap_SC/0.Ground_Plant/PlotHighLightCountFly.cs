using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlotHighLightCountFly : FarmingPlantAbstract
{
    [SerializeField] protected TextMeshProUGUI _txtCountFly;

    [SerializeField] protected SpriteRenderer _SP_Rep_IconFly;

    [SerializeField] protected float _Time_delay = 0.5f;
    [SerializeField] protected float _Timer = 0;
    [SerializeField] protected float _Speed_Fly = 4.5f;
    [SerializeField] protected bool _IsFlying;
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadTextCountFly();
        this.LoadSpriteRenderIconFlyRepresent();
    }

    protected virtual void LoadSpriteRenderIconFlyRepresent()
    {
        if (this._SP_Rep_IconFly != null) return;

        this._SP_Rep_IconFly = GameObject.Find("SpriteRep_ItemFly").GetComponent<SpriteRenderer>();
        if (this._SP_Rep_IconFly == null) return;
        this._SP_Rep_IconFly.gameObject.SetActive(false);
    }

    protected virtual void LoadTextCountFly()
    {
        if (this._txtCountFly != null) return;

        this._txtCountFly = GameObject.Find("txtCount").GetComponent<TextMeshProUGUI>();
        if (this._txtCountFly == null) return;
        this._txtCountFly.gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _Timer = 0f;
        _IsFlying = false;
        this.RefreshPlotHighLightFly();

        PlayerCtrl.Instance.PlayerInventory.OnItemAmountChanged += ShowTextFly;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        PlayerCtrl.Instance.PlayerInventory.OnItemAmountChanged -= ShowTextFly;
    }

    private void Update()
    {
        if (!_IsFlying)
            return;

        _Timer += Time.deltaTime;

        transform.position += Vector3.up * _Speed_Fly * Time.deltaTime;

        if (_Timer >= _Time_delay) this.RefreshPlotHighLightFly();

    }

    public virtual void ShowTextFly(ItemType type, int amount)
    {
        this._txtCountFly.gameObject.SetActive(true);
        _txtCountFly.text = amount < 0
            ? amount.ToString()
            : "+" + amount;

        this._SP_Rep_IconFly.gameObject.SetActive(true);
        _SP_Rep_IconFly.sprite =
            PlayerCtrl.Instance.PlayerInventory
                .GetItemDateByType(type)
                .ItemDefinitionSO.Icon;

        _Timer = 0f;
        _IsFlying = true;
    }
    protected virtual void RefreshPlotHighLightFly()
    {
        this._txtCountFly.gameObject.SetActive(false);
        this._SP_Rep_IconFly.gameObject.SetActive(false);
        this.transform.localPosition = Vector3.zero;

    }    
}
