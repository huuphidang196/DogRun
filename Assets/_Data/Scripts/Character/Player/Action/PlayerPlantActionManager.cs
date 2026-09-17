using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantActionManager : PlayerActionManagerAbstract, IPlayerAction
{
    public PlayerActionType PlayerActionType => PlayerActionType.Plant;

    //[SerializeField]
    //protected bool isChangingState = true;

    //Can harvest
    public bool CanExecute()
    {
        return true;
    }

    public void Execute()
    {
        //Plant
        PlotData currentPlot = FarmingPlantManager.Instance.CurrentPlotData;

        currentPlot.State = currentPlot.State == PlotState.Harvestable && this.UserWantToHarvest() ?
            PlotState.Empty : (PlotState)((int)currentPlot.State + 1);

        if (currentPlot.State == PlotState.Plowed) this.PlowedSeed();

        currentPlot.GrowthStage = GrowthStage.Seed;
        // if (currentPlot.CropSO == null) currentPlot.CropSO = 
        FarmingPlantManager.Instance.GroundPlant.Plant(currentPlot.Origin, currentPlot.CropSO, currentPlot.GrowthStage);

    }

    protected virtual void PlowedSeed()
    {
        ItemType selectedTypeItem = GameDataManager.Instance.CropPlantUsing;

        switch (selectedTypeItem)
        {
            case ItemType.None:
                break;
            case ItemType.RiceSeeds:
                FarmingPlantManager.Instance.CurrentPlotData.CropSO = FarmingPlantManager.Instance.CropSO_Needed(CropType.Rice);
                break;
            case ItemType.PumpkinSeeds:
                FarmingPlantManager.Instance.CurrentPlotData.CropSO = FarmingPlantManager.Instance.CropSO_Needed(CropType.Pumpkin);
                break;
            case ItemType.CabbageSeeds:
                FarmingPlantManager.Instance.CurrentPlotData.CropSO = FarmingPlantManager.Instance.CropSO_Needed(CropType.Cabbage);
                break;
        }
        //minus seed
        this._PlayerActionManager.PlayerCtrl.PlayerInventory.RemoveItem(selectedTypeItem);
    }

    protected virtual bool UserWantToHarvest()
    {
        PlotData currentPlotHarvestable = FarmingPlantManager.Instance.CurrentPlotData;

        if (currentPlotHarvestable == null || currentPlotHarvestable.State != PlotState.Harvestable) return false;

        switch (currentPlotHarvestable.CropSO.CropType)
        {
            case CropType.None:
                break;
            case CropType.Rice:
                this._PlayerActionManager.PlayerCtrl.PlayerInventory.AddItem(ItemType.HarvestedRice, currentPlotHarvestable.CropSO.HarvestAmount);
                break;
            case CropType.Pumpkin:
                this._PlayerActionManager.PlayerCtrl.PlayerInventory.AddItem(ItemType.HarvestedPumpkin, currentPlotHarvestable.CropSO.HarvestAmount);
                break;
            case CropType.Cabbage:
                this._PlayerActionManager.PlayerCtrl.PlayerInventory.AddItem(ItemType.HarvestedPumpkin, currentPlotHarvestable.CropSO.HarvestAmount);
                break;
        }

   
        return true;

    }
}