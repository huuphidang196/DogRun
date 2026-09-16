using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlotData
{
    public Vector3Int Origin;

    public PlotState State;

    public CropDefinitionSO CropSO;

    public GrowthStage GrowthStage;

    public float GrowTimer;
}
public enum PlotState
{
    Empty = 0,          // Chưa cuốc
    Plowed = 1,         // Đã cuốc đất
    Growing = 2,        // Đang trồng
    Harvestable = 3     // Có thể thu hoạch
}
public enum GrowthStage
{
    Seed = 0,
    Growing = 1,
    Mature = 2,
}