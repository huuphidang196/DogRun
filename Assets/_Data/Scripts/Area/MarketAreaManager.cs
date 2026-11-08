using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketAreaManager : StandMonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoxCollider2D _MarketCollider;

    [Header("Bool")]
    [SerializeField] protected bool isPurchasing = false;
    public bool IsPurchasing => this.isPurchasing;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMarketCollider();
    }

    protected virtual void LoadMarketCollider()
    {
        if (this._MarketCollider != null) return;

        this._MarketCollider = GetComponentInChildren<BoxCollider2D>();
    }

    public virtual bool CheckPlayerInMarketArea()
    {
        if (this._MarketCollider == null) return false;
        if (PlayerCtrl.Instance == null) return false;

        return this._MarketCollider.OverlapPoint(
            PlayerCtrl.Instance.transform.position
        );
    }
}
