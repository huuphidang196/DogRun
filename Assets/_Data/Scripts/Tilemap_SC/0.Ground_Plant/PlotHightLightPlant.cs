using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotHightLightPlant : StandMonoBehaviour
{
    [SerializeField] protected FarmingPlantManager _FarmingPlantManager;

    [SerializeField] protected Transform UI_Type_Action_Plant;
    [SerializeField] protected BoxCollider2D _StateArea;
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadFarmingPlantManager();
        this.LoadUITypeActionPlant();
        this.LoadCollider2DStateArea();
    }

    protected virtual void LoadCollider2DStateArea()
    {
        if (this._StateArea != null || this.UI_Type_Action_Plant == null) return;

        this._StateArea = this.UI_Type_Action_Plant.GetComponent<BoxCollider2D>();
    }

    protected virtual void LoadUITypeActionPlant()
    {
        if (this.UI_Type_Action_Plant != null) return;

        this.UI_Type_Action_Plant = transform.Find("UI_Type_Action_Plant");
    }

    protected virtual void LoadFarmingPlantManager()
    {
        if (this._FarmingPlantManager != null) return;

        this._FarmingPlantManager = GetComponentInParent<FarmingPlantManager>();
    }

    private void Update()
    {
        this.UpdateElementsHightLightChangeState();

        if (!this._FarmingPlantManager.AllowChangeState) return;

        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began) return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position);

        // Không nằm trong khu vực HightLight
        if (!this._StateArea.OverlapPoint(worldPos)) return;
     
        // Chọn thửa này
        this.NotifyForManagerChangeState();
    }

    protected virtual void NotifyForManagerChangeState()
    {
        //user want to change state
        this._FarmingPlantManager.GroundPlant.ChangePlotStateByUserAction();

    }

    protected virtual void UpdateElementsHightLightChangeState()
    {
        //Change pos
        this.transform.position = this._FarmingPlantManager.CurrentPlotData.Origin;
       // Debug.Log("Pos: " + this.transform.position + ", origin : " + this._FarmingPlantManager.CurrentPlotData.Origin);
       // if (this.UI_Type_Action_Plant.gameObject.activeInHierarchy == this._FarmingPlantManager.AllowChangeState) return;

        this.ChangeStateElementsHightLight();
    }

    protected virtual void ChangeStateElementsHightLight()
    {
        this.UI_Type_Action_Plant.gameObject.SetActive(this._FarmingPlantManager.AllowChangeState);

        //Update Rep Icon
        foreach (Transform item in this.UI_Type_Action_Plant)
        {
            item.gameObject.SetActive(false);
        }

        if (!this._FarmingPlantManager.AllowChangeState) return;

        int index_Icon = this._FarmingPlantManager.CurrentPlotData.State != PlotState.Harvestable ? (int)this._FarmingPlantManager.CurrentPlotData.State : 2;
        
        if (index_Icon > this.UI_Type_Action_Plant.childCount) return;

        this.UI_Type_Action_Plant.GetChild(index_Icon).gameObject.SetActive(true);
    }
}
