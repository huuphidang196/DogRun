using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCtrlAbstract : StandMonoBehaviour
{
    [SerializeField] protected ObjCtrl _ObjCtrl;
    public ObjCtrl ObjCtrl => this._ObjCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadObjCtrl();
    }

    protected virtual void LoadObjCtrl()
    {
        if (this._ObjCtrl != null) return;

        this._ObjCtrl = GetComponentInParent<ObjCtrl>();

        if (this._ObjCtrl != null) return;

        this._ObjCtrl = transform.parent.GetComponent<ObjCtrl>();
    }
}
