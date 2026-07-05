using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjMovement : ObjCtrlAbstract
{
    [SerializeField] protected Rigidbody2D _Rig2D;
    public Rigidbody2D Rigidbody2D => this._Rig2D;

    [SerializeField] protected BoxCollider2D _Collider2D;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadRigidbody2D();
        this.LoadBoxCollider2D();
    }

    protected virtual void LoadBoxCollider2D()
    {
        if (this._Collider2D != null) return;

        this._Collider2D = this._ObjCtrl.GetComponent<BoxCollider2D>();
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this._Rig2D != null) return;

        this._Rig2D = this._ObjCtrl.GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        this._Rig2D.velocity = this.ObjCtrl.ObjInputMovement.OutputMovement();
    }
}
