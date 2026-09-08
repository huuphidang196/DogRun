using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingPlantManager : StandMonoBehaviour
{
    [SerializeField] protected List<PlotData> _Plots;

    [SerializeField] protected List<CropDefinitionSO> _List_CropSO = new();
    [SerializeField]
    private GroundPlant m_GroundPlant;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadAllCropDefinitionSO();
        this.LoadGroundPlant();
    }

    protected virtual void LoadGroundPlant()
    {
        if (this.m_GroundPlant != null) return;

        this.m_GroundPlant = GetComponent<GroundPlant>();
    }

    protected virtual void LoadAllCropDefinitionSO()
    {
        if (this._List_CropSO.Count > 0) return;

        _List_CropSO.Clear();

        CropDefinitionSO[] cropSOs =
            Resources.LoadAll<CropDefinitionSO>("SO/Tilemap");

        _List_CropSO.AddRange(cropSOs);
    }
}


