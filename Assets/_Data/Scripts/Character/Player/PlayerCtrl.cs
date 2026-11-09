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

    [SerializeField] protected PlayerActionManager _PlayerActionManager;
    public PlayerActionManager PlayerActionManager => this._PlayerActionManager;

    [SerializeField] protected PlayerWallet _PlayerWallet;
    public PlayerWallet PlayerWallet => this._PlayerWallet;

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
        this.LoadPlayerActionManager();
        this.LoadPlayerWallet();
    }

    protected virtual void LoadPlayerWallet()
    {
        if (this._PlayerWallet != null) return;

        this._PlayerWallet = GetComponentInChildren<PlayerWallet>();
    }

    protected virtual void LoadPlayerActionManager()
    {
        if (this._PlayerActionManager != null) return;

        this._PlayerActionManager = GetComponentInChildren<PlayerActionManager>();
    }

    protected virtual void LoadPlayerInventory()
    {
        if (this._PlayerInventory != null) return;

        this._PlayerInventory = GetComponentInChildren<PlayerInventory>();
    }
}
