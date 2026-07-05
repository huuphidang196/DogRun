using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowButtonUIActive : ActiveUIButton
{
    protected override void CheckStartCooldown()
    {
        if (!InputManagerInstance.Instance.IsPlayerThrow()) return;

        this._cooldownTime = InputManagerInstance.Instance.GetCooldownTimeByTypeButtonActive(this._ButtonUIActiveParent.TypeButtonActive);
      
        this.StartCooldown();
    }
}
