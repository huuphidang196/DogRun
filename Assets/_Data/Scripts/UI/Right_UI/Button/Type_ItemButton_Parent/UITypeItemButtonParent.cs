using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITypeItemButtonParent : StandMonoBehaviour
{
    [SerializeField] protected List<ButtonSelectItemThrowManager> _Buttons;
    [SerializeField] protected ItemType _DefaultSelected = ItemType.Slipper;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButtons();
    }

    protected virtual void LoadButtons()
    {
        if (this._Buttons != null && this._Buttons.Count > 0) return;
        this._Buttons = new List<ButtonSelectItemThrowManager>(GetComponentsInChildren<ButtonSelectItemThrowManager>());
    }

    protected override void Start()
    {
        base.Start();
        this.SelectItem(_DefaultSelected); // set mặc định lúc mở game (VD: Slipper sáng sẵn)
    }

    // 👉 Được gọi từ UnityEvent OnClick của mỗi button trong Inspector
    public virtual void SelectItem(ItemType type)
    {
        // 1. Báo cho Spawner biết loại item đang chọn
        PlayerWeaponSpawner.Instance.SelectThrowItem(type);

        // 2. Cập nhật trạng thái sáng/tắt cho TẤT CẢ button — chỉ đúng 1 cái sáng tại 1 thời điểm
        foreach (ButtonSelectItemThrowManager btn in this._Buttons)
        {
            btn.SetSelected(btn.ItemType == type);
        }
    }
}
