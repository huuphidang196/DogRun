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

    public int GrowthStage;
}
public enum PlotState
{
    Empty,          // Chưa cuốc
    Plowed,         // Đã cuốc đất
    Growing,        // Đang trồng
    Harvestable     // Có thể thu hoạch
}
public enum HighlightType
{
    None,
    EmptyPlot,
    Plant,
    Harvest
}