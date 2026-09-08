using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCtrl : StandMonoBehaviour
{
    [SerializeField] protected ObjInputMovement _ObjInputMovement;
    public ObjInputMovement ObjInputMovement => this._ObjInputMovement;

    [SerializeField] protected ObjMovement _ObjMovement;
    public ObjMovement ObjMovement => this._ObjMovement;

    [SerializeField] protected ObjModel _ObjModel;
    public ObjModel ObjModel => this._ObjModel;

    [SerializeField] protected ObjDespawnAbstract _ObjDespawnAbstract;
    public ObjDespawnAbstract ObjDespawnAbstract => this._ObjDespawnAbstract;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadObjInputMovement();
        this.LoadObjMovement();
        this.LoadObjModel();
        this.LoadObjDespawnAbstract();
    }

    protected virtual void LoadObjDespawnAbstract()
    {
        if (this._ObjDespawnAbstract != null) return;

        this._ObjDespawnAbstract = GetComponentInChildren<ObjDespawnAbstract>();
    }

    protected virtual void LoadObjModel()
    {
        if (this._ObjModel != null) return;

        this._ObjModel = GetComponentInChildren<ObjModel>();
    }

    protected virtual void LoadObjMovement()
    {
        if (this._ObjMovement != null) return;

        this._ObjMovement = GetComponentInChildren<ObjMovement>();
    }

    protected virtual void LoadObjInputMovement()
    {
        if (this._ObjInputMovement != null) return;

        this._ObjInputMovement = GetComponentInChildren<ObjInputMovement>();
    }
}
