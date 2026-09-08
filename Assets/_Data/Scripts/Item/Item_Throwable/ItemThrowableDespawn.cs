using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowableDespawn : ObjDespawnByTime
{
    // 👉 Class con override để chạy animation/VFX ngay khi TRIGGER despawn, trước khi object thực sự bị tắt
    protected override void OnDespawnTriggered()
    {
        //Spawn VFX

    }
}
