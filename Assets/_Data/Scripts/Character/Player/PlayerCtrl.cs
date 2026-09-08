using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : ObjCtrl
{
    public static PlayerCtrl Instance;

    [SerializeField] protected PlayerInventory _PlayerInventory;
    public PlayerInventory PlayerInventory => this._PlayerInventory;

    public PlayerModel PlayerModel => this._ObjModel as PlayerModel;

    protected override void Awake()
    {
        base.Awake();

        if (Instance != null) return;

        Instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadPlayerInventory();
    }

    protected virtual void LoadPlayerInventory()
    {
        if (this._PlayerInventory != null) return;

        this._PlayerInventory = GetComponentInChildren<PlayerInventory>();
    }
}
