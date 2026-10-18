using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeFishingManager : StandMonoBehaviour
{
    [SerializeField] protected PolygonCollider2D _Fishing_Area_Collider;
    [SerializeField] protected bool isIntoFishing = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadFishingAreaCollider();
    }

    protected virtual void LoadFishingAreaCollider()
    {
        if (this._Fishing_Area_Collider != null) return;

        this._Fishing_Area_Collider = GetComponentInChildren<PolygonCollider2D>();
    }

    public virtual bool CheckPlayerInPlotFishingArea() => this._Fishing_Area_Collider.OverlapPoint(PlayerCtrl.Instance.transform.position);

}
