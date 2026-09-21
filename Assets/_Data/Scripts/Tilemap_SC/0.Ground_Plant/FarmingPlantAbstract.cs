using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingPlantAbstract : StandMonoBehaviour
{
    [SerializeField] protected FarmingPlantManager _FarmingPlantManager;
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadFarmingPlantManager();
    }

    protected virtual void LoadFarmingPlantManager()
    {
        if (this._FarmingPlantManager != null) return;

        this._FarmingPlantManager = GetComponentInParent<FarmingPlantManager>();
    }
}
