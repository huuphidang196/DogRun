using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingPlantManager : Singleton<FarmingPlantManager>
{
    [SerializeField] protected List<CropDefinitionSO> _List_CropSO = new();
    public CropDefinitionSO CropSO_Needed(CropType cropType) => this._List_CropSO.Find(x => x.CropType == cropType);

    [SerializeField] protected List<PlotData> _Plots;
    private const int Count_Plot = 3;

    [SerializeField] protected GroundPlant _GroundPlant;
    public GroundPlant GroundPlant => this._GroundPlant;

    [SerializeField] protected PlotHightLightPlant _PlotHightLightPlant;

    [SerializeField] protected PolygonCollider2D m_FarmArea;

    [SerializeField] protected float m_HighlightDistance = 2.8f;

    [SerializeField] protected PlotData _CurrentPlot;
    public PlotData CurrentPlotData => this._CurrentPlot;

    [SerializeField] protected bool allowedChangeState = false;
    public bool AllowChangeState => this.allowedChangeState;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadAllCropDefinitionSO();
        this.LoadFarmAreaCollider2D();
        this.LoadGroundPlant();
        this.LoadPlotHightLightPlant();
        this.LoadPlotsInitilize();
    }

    protected virtual void LoadPlotHightLightPlant()
    {
        if (this._PlotHightLightPlant != null) return;

        this._PlotHightLightPlant = GetComponentInChildren<PlotHightLightPlant>();
    }

    protected virtual void LoadFarmAreaCollider2D()
    {
        if (this.m_FarmArea != null) return;

        this.m_FarmArea = GetComponent<PolygonCollider2D>();
    }

    protected virtual void LoadPlotsInitilize()
    {
        if (this._Plots.Count > 0) return;

        for (int i = 0; i < Count_Plot; i++)
        {
            for (int j = 0; j < Count_Plot; j++)
            {
                PlotData plot = new PlotData();
                plot.Origin = new Vector3Int(-40 + 6 * i, 22 + 6 * j, 0);
                plot.State = PlotState.Empty;
                plot.CropSO = null;
                plot.GrowthStage = 0;

                this._Plots.Add(plot);
            }
        }
    }

    protected virtual void LoadGroundPlant()
    {
        if (this._GroundPlant != null) return;

        this._GroundPlant = GetComponent<GroundPlant>();
    }

    protected virtual void LoadAllCropDefinitionSO()
    {
        if (this._List_CropSO.Count > 0) return;

        _List_CropSO.Clear();

        CropDefinitionSO[] cropSOs =
            Resources.LoadAll<CropDefinitionSO>("SO/Tilemap");

        _List_CropSO.AddRange(cropSOs);
    }

    protected virtual void Update()
    {
        this.ConsiderAllowChangeState();

        this.UpdateCropGrowth();

        this.CheckPlayerPlotWantChangeState();

        if (this._PlotHightLightPlant.gameObject.activeInHierarchy == (_CurrentPlot != null)) return;

        this._PlotHightLightPlant.gameObject.SetActive(_CurrentPlot != null);

    }

    protected virtual void ConsiderAllowChangeState()
    {
        this.allowedChangeState = false;

        if (this._CurrentPlot != null)
        {
            bool isGrowing =
                this._CurrentPlot.State == PlotState.Growing &&
                this._CurrentPlot.GrowthStage != GrowthStage.Mature;

            bool isEmptyWithoutSeed =
                this._CurrentPlot.State == PlotState.Empty &&
                !PlayerCtrl.Instance.PlayerInventory.HasPlantableItem();

            this.allowedChangeState = !isGrowing && !isEmptyWithoutSeed;
        }
    }

    private void UpdateCropGrowth()
    {
        foreach (PlotData plot in _Plots)
        {
            if (plot.State != PlotState.Growing)
                continue;

            if (plot.CropSO == null)
                continue;

            if (plot.GrowthStage == GrowthStage.Mature)
                continue;

            plot.GrowTimer += Time.deltaTime;

            if (plot.GrowTimer >= plot.CropSO.GrowTime)
            {
                plot.GrowTimer = 0f;

                if (plot.GrowthStage == GrowthStage.Seed)
                {
                    plot.GrowthStage = GrowthStage.Growing;
                }
                else if (plot.GrowthStage == GrowthStage.Growing)
                {
                    plot.GrowthStage = GrowthStage.Mature;
                    plot.State = PlotState.Harvestable;
                }
                // if (currentPlot.CropSO == null) currentPlot.CropSO = 
                this.GroundPlant.Plant(plot.Origin, plot.CropSO, plot.GrowthStage);
            }
        }
    }

    public virtual bool CheckPlayerInPlot() => this.m_FarmArea.OverlapPoint(PlayerCtrl.Instance.transform.position);
    public virtual void CheckPlayerPlotWantChangeState()
    {
        Vector2 playerPos = PlayerCtrl.Instance.transform.position;

        // Không nằm trong khu vực ruộng
        if (!m_FarmArea.OverlapPoint(playerPos))
        {
            if (_CurrentPlot != null)
            {
                _CurrentPlot = null;
            }

            return;
        }

        // Tìm plot gần nhất
        PlotData nearestPlot = null;
        float minDistance = float.MaxValue;

        foreach (PlotData plot in _Plots)
        {
            Vector2 plotCenter = (Vector2Int)plot.Origin + new Vector2(1.5f, 1.5f);

            float distance = Vector2.Distance(playerPos, plotCenter);

            if (distance <= m_HighlightDistance && distance < minDistance && distance >= 2f)
            {
                minDistance = distance;
                nearestPlot = plot;
            }
        }

        // Không có plot gần
        if (nearestPlot == null)
        {
            _CurrentPlot = null;
            return;
        }

        // Vẫn ở plot cũ
        if (_CurrentPlot == nearestPlot)
            return;

        _CurrentPlot = nearestPlot;
    }


}


