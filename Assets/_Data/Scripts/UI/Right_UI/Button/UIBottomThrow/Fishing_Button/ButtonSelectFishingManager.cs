using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectFishingManager : ButtonSelectItemManager
{
    [SerializeField]
    private bool _lastInto = false;
    [SerializeField]
    private bool _lastAuto = false;

    private void Update()
    {
        RefreshState();
    }

    public void RefreshState()
    {
        bool into = AreaManager.Instance.LakeFishingManager.CheckPlayerInPlotFishingArea();
        bool auto = AreaManager.Instance.LakeFishingManager.IsAutoFishing;

        // Chỉ cập nhật khi trạng thái thay đổi.
        if (into != _lastInto || auto != _lastAuto)
        {
            UpdateFishingObjects(into);
            UpdateLight(auto);

            _lastInto = into;
            _lastAuto = auto;
        }
    }

    private void UpdateFishingObjects(bool into)
    {
        foreach (GameObject obj in this.transform)
        {
            if (obj == null) continue;

            obj.SetActive(into);
        }
    }

    private void UpdateLight(bool auto)
    {
        if (this._Image_Light == null) return;

        this._Image_Light.SetActive(auto);
    }
}
