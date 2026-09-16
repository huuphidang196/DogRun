using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundPlant : StandMonoBehaviour
{
    [SerializeField] protected FarmingPlantManager _FarmingPlantManager;
    [SerializeField] private Tilemap _Tilemap;
    public Tilemap Tilemap_Plant => this._Tilemap;

    private const int PlotSize = 3;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadFarmingPlantManager();
        this.LoadTilemapPlant();
    }

    protected virtual void LoadTilemapPlant()
    {
        if (this._Tilemap != null) return;

        this._Tilemap = GetComponent<Tilemap>();
    }

    protected virtual void LoadFarmingPlantManager()
    {
        if (this._FarmingPlantManager != null) return;

        this._FarmingPlantManager = GetComponentInParent<FarmingPlantManager>();
    }
    public virtual void Plant(Vector3Int origin, CropDefinitionSO crop, GrowthStage growthStage)
    {
        if (crop == null)
            return;

        TileBase tiles = GetStageTiles(crop, growthStage);

        if (tiles == null)
            return;

        for (int y = 0; y < PlotSize; y++)
        {
            for (int x = 0; x < PlotSize; x++)
            {
                Vector3Int cell = origin + new Vector3Int(x, y, 0);

                _Tilemap.SetTile(cell, tiles);
            }
        }
    }

    private TileBase GetStageTiles(CropDefinitionSO crop, GrowthStage growthStage)
    {
        return growthStage switch
        {
            GrowthStage.Seed => crop.SeedTile,
            GrowthStage.Growing => crop.GrowingTile,
            GrowthStage.Mature => crop.MatureTile,
            _ => null
        };
    }

    public virtual void ClearPlot(Vector3Int origin)
    {
        for (int y = 0; y < PlotSize; y++)
        {
            for (int x = 0; x < PlotSize; x++)
            {
                Vector3Int cell = origin + new Vector3Int(x, y, 0);

                _Tilemap.SetTile(cell, null);
            }
        }
    }

    public virtual void ChangePlotStateByUserAction()
    {
        //Certainly user can change state

        if (this._FarmingPlantManager.CurrentPlotData.State >= PlotState.Harvestable)
        {
            PlayerCtrl.Instance.PlayerActionManager.UserWantToHarvest();
            return;
        }

        this._FarmingPlantManager.CurrentPlotData.State = (PlotState)((int)this._FarmingPlantManager.CurrentPlotData.State + 1);

        //Plant
        PlotData currentPlot = this._FarmingPlantManager.CurrentPlotData;
       // if (currentPlot.CropSO == null) currentPlot.CropSO = 
        this.Plant(currentPlot.Origin, currentPlot.CropSO, currentPlot.GrowthStage);

    }
}
