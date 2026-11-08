using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPurchasingManager : StandMonoBehaviour
{
    [SerializeField] protected ItemExchangeTransaction _ItemExchangeTransaction;
    public ItemExchangeTransaction ItemExchangeTransaction => this._ItemExchangeTransaction;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadItemExchangeTransaction();
    }

    protected virtual void LoadItemExchangeTransaction()
    {
        if (this._ItemExchangeTransaction != null) return;

        this._ItemExchangeTransaction = transform.GetComponentInChildren<ItemExchangeTransaction>();
    }
}
