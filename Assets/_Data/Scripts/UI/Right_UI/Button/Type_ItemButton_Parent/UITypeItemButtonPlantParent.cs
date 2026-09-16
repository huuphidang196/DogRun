using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITypeItemButtonPlantParent : UITypeItemButtonParent
{
    // 👉 Được gọi từ UnityEvent OnClick của mỗi button trong Inspector
    public override void SelectItem(ItemType type)
    {
        GameDataManager.Instance.SetCropPlantUsing(type);

        base.SelectItem(type);
    }
    
    protected virtual void Update()
    {
        //inactive with anyButton that equivalent SO_Seed zero

        //auto transfer next ItemType
    }    
       
}
