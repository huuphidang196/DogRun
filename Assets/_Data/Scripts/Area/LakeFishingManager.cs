using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeFishingManager : StandMonoBehaviour
{
    [Header("References")]
    [SerializeField] protected PolygonCollider2D _Fishing_Area_Collider;
    [SerializeField] private FishingConfigSO _fishingConfigSO;

    [Header("Bool")]
    private bool _isFishing;
    [SerializeField] protected bool isAutoFishing = false;
    public bool IsAutoFishing => this.isAutoFishing;

    [Header("OutSide")]
    [SerializeField] protected Vector3 _Pos_Player_Fishing = new Vector3(32f, -28f, 0);

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadFishingAreaCollider();
        this.LoadFishingConfigSO();
    }

    protected virtual void LoadFishingConfigSO()
    {
        if (this._fishingConfigSO != null) return;

        this._fishingConfigSO = Resources.Load<FishingConfigSO>("SO/Area/FishingConfigSO");
    }

    protected virtual void LoadFishingAreaCollider()
    {
        if (this._Fishing_Area_Collider != null) return;

        this._Fishing_Area_Collider = GetComponentInChildren<PolygonCollider2D>();
    }

    public virtual bool CheckPlayerInPlotFishingArea() => this._Fishing_Area_Collider.OverlapPoint(PlayerCtrl.Instance.transform.position);

    public virtual bool SetAutoFishing() => this.isAutoFishing = !this.isAutoFishing;


    private void Update()
    {
        RefreshFishingState();
    }

    private void RefreshFishingState()
    {
        bool canFish = this.isAutoFishing && this.CheckPlayerInPlotFishingArea();

        if (canFish && !_isFishing)
        {
            StartFishing();
        }
        else if (!canFish && _isFishing)
        {
            StopFishing();
        }
    }

    private void StartFishing()
    {
        _isFishing = true;

        ScheduleNextCatch();
    }

    private void StopFishing()
    {
        _isFishing = false;

        CancelInvoke(nameof(CatchFish));
    }

    private void ScheduleNextCatch()
    {
        if (!_isFishing) return;

        //Set pos
        PlayerCtrl.Instance.transform.position = this._Pos_Player_Fishing;

        float waitTime = _fishingConfigSO.RandomWaitTime;
        Invoke(nameof(CatchFish), waitTime);
    }

    private void CatchFish()
    {
        if (!_isFishing ||
            !this.isAutoFishing ||
            !this.CheckPlayerInPlotFishingArea())
        {
            StopFishing();
            return;
        }

        int fishAmount = _fishingConfigSO.RandomFishAmount;

        PlayerCtrl.Instance.PlayerInventory.AddItem(
            ItemType.Fish,
            fishAmount
        );

        // Lên lịch cho lượt câu tiếp theo.
        ScheduleNextCatch();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CancelInvoke(nameof(CatchFish));
        _isFishing = false;
    }
}
