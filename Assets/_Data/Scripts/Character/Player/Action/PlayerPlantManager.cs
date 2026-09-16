using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantManager : PlayerActionManagerAbstract, IPlayerAction
{
    public PlayerActionType PlayerActionType => PlayerActionType.Plant;

   //Can harvest
    public bool CanExecute()
    {
        return FarmingPlantManager.Instance.CheckPlayerInPlot();
    }

    public void Execute()
    {
        PlotData currentPlotHarvestable = FarmingPlantManager.Instance.CurrentPlotData;

        if (currentPlotHarvestable == null || currentPlotHarvestable.State != PlotState.Harvestable) return;

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

        FarmingPlantManager.Instance.CurrentPlotData.State = PlotState.Empty;
    }
}