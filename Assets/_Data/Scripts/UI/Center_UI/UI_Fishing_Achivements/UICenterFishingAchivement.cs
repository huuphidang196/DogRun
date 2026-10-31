using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICenterFishingAchivement : StandMonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextShowCountFishAchivement _txt_fishCountText;

    [Header("Settings")]
    [SerializeField] private ItemType itemType = ItemType.Fish;

    [SerializeField] protected float _TimeDelayDisablePanel = 3f;

    protected override void ResetValue()
    {
        base.ResetValue();

        this.ScheduleDisablePanel();
    }

    protected override void Start()
    {
        base.Start();
        LoadInventory();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        UnsubscribeInventoryEvent();
    }

    private void LoadInventory()
    {
        if (PlayerCtrl.Instance == null) return;

        if (PlayerCtrl.Instance.PlayerInventory == null) return;

        PlayerCtrl.Instance.PlayerInventory.OnItemAmountChanged += HandleItemAmountChanged;
    }

    private void UnsubscribeInventoryEvent()
    {
        if (PlayerCtrl.Instance == null) return;

        if (PlayerCtrl.Instance.PlayerInventory == null) return;

        PlayerCtrl.Instance.PlayerInventory.OnItemAmountChanged -= HandleItemAmountChanged;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadTextShowCountFishAchivement();
    }

    protected virtual void LoadTextShowCountFishAchivement()
    {
        this.RefreshStatePanel(true);

        if (this._txt_fishCountText != null) return;

        this._txt_fishCountText = GetComponentInChildren<TextShowCountFishAchivement>();


    }

    private void HandleItemAmountChanged(ItemType type, int amountChanged)
    {
        if (type != itemType) return;

        RefreshCount(amountChanged);
    }

    private void RefreshCount(int amountChanged)
    {
        if (PlayerCtrl.Instance == null) return;

        if (PlayerCtrl.Instance.PlayerInventory == null) return;

        if (amountChanged == 0) return;

        this._txt_fishCountText.SetText("x" + amountChanged.ToString());

        this.RefreshStatePanel(true);

        Invoke(nameof(this.ScheduleDisablePanel), this._TimeDelayDisablePanel);
    }

    protected virtual void RefreshStatePanel(bool active)
    {
        foreach (Transform item in this.transform)
        {
            item.gameObject.SetActive(active);
        }
    }
    private void ScheduleDisablePanel()
    {
        this.RefreshStatePanel(false);
    }
}
