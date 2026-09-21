using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBottomPlantManager : StandMonoBehaviour
{
    [SerializeField] protected Transform _Scroll_UI_Plant;
    [SerializeField] protected bool isPlantable = true;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadScrollUIPlant();
    }

    protected virtual void LoadScrollUIPlant()
    {
        if (this._Scroll_UI_Plant != null) return;

        this._Scroll_UI_Plant = transform.Find("Scroll_UI_Plant");
    }

    protected virtual void FixedUpdate()
    {
        this.isPlantable = FarmingPlantManager.Instance.CheckPlayerInPlot() && PlayerCtrl.Instance.PlayerInventory.HasPlantableItem();

        if (this._Scroll_UI_Plant.gameObject.activeInHierarchy == this.isPlantable) return;

        this._Scroll_UI_Plant.gameObject.SetActive(this.isPlantable);
    }
}
