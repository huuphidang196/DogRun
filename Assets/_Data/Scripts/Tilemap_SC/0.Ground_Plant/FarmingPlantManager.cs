using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingPlantManager : StandMonoBehaviour
{
    [SerializeField] protected List<PlotData> _Plots;
    protected const int Count_Plot = 3;

    [SerializeField] protected List<CropDefinitionSO> _List_CropSO = new();
    [SerializeField]
    private GroundPlant m_GroundPlant;
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadAllCropDefinitionSO();
        this.LoadGroundPlant();

        this.LoadPlotsInitilize();
    }

    protected virtual void LoadPlotsInitilize()
    {
        if (this._Plots.Count > 0) return;

        for (int i = 0; i < Count_Plot; i++)
        {
            for (int j = 0; j < Count_Plot; j++)
            {
                PlotData plot = new PlotData();
                plot.Origin = new Vector3Int(-40 + 3 * i, 23 + 3 * j, 0);
                plot.State = PlotState.Empty;
                plot.CropSO = null;
                plot.GrowthStage = 0;

                this._Plots.Add(plot);
            }
        }
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


