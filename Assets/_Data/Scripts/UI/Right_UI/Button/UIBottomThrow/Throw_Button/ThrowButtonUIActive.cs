using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowButtonUIActive : ActiveUIButton
{
    public ThrowButtonUIActiveParent ThrowButtonUIActiveParent => this._ButtonUIActiveParent as ThrowButtonUIActiveParent;
    protected override void Update()
    {
        base.Update();

        this.ProcessThrowOnlyHasItem();
      
    }

    protected virtual void ProcessThrowOnlyHasItem()
    {
        if (this.isCoolingDown) return;

        if (this._ButtonUIActiveParent.ButtonUIStandard.BtnExect.interactable == PlayerCtrl.Instance.PlayerInventory.HasItemThrowable()) return;

        this._ButtonUIActiveParent.ButtonUIStandard.BtnExect.interactable = PlayerCtrl.Instance.PlayerInventory.HasItemThrowable();
        this.ThrowButtonUIActiveParent.Image_UsingThrow.gameObject.SetActive(PlayerCtrl.Instance.PlayerInventory.HasItemThrowable());
    }

    protected override void CheckStartCooldown()
    {
        if (!GameDataManager.Instance.IsPlayerThrow()) return;

        this._cooldownTime = GameDataManager.Instance.GetCooldownTimeByTypeButtonActive(this._ButtonUIActiveParent.TypeButtonActive);
      
        this.StartCooldown();
    }
}
