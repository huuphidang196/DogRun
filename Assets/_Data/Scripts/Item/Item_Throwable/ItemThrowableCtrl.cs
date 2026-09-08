using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowableCtrl : ObjCtrl
{
    public ItemThrowableInputMovement ItemThrowableInputMovement => this._ObjInputMovement as ItemThrowableInputMovement;

    [SerializeField] protected ItemData _ItemData;

    [SerializeField] protected ItemThrowableDespawn _ItemThrowableDespawn;
    public ItemThrowableDespawn ItemThrowableDespawn => this._ItemThrowableDespawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadItemData();
        this.LoadItemThrowableDespawn();
    }

    protected virtual void LoadItemData()
    {
        if (this._ItemData.Amount != 0) return;

        ItemDefinition definition = Resources.Load<ItemDefinition>("SO/Item/" + this.gameObject.name);
        this._ItemData = new ItemData(definition, 1);
    }

    protected virtual void LoadItemThrowableDespawn()
    {
        if (this._ItemThrowableDespawn !=  null) return;

        this._ItemThrowableDespawn = GetComponentInChildren<ItemThrowableDespawn>();
    }

}
