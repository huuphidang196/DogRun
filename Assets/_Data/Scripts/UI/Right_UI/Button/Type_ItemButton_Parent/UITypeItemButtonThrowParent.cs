using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITypeItemButtonThrowParent : UITypeItemButtonParent
{
    // 👉 Được gọi từ UnityEvent OnClick của mỗi button trong Inspector
    public override void SelectItem(ItemType type)
    {
        // 1. Báo cho Spawner biết loại item đang chọn
        PlayerWeaponSpawner.Instance.SelectThrowItem(type);

        base.SelectItem(type);
    }
}
