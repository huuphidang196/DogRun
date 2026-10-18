using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITypeItemButtonParent : StandMonoBehaviour
{
    [SerializeField] protected List<ButtonSelectItemManager> _Buttons;
    [SerializeField] protected ItemType _CurrentSelected = ItemType.Slipper;
    public ItemType CurrentItem_Selected => this._CurrentSelected;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButtons();
    }

    protected virtual void LoadButtons()
    {
        if (this._Buttons != null && this._Buttons.Count > 0) return;
        this._Buttons = new List<ButtonSelectItemManager>(GetComponentsInChildren<ButtonSelectItemManager>());
    }

    protected override void Start()
    {
        base.Start();
        this.SelectItem(_CurrentSelected); // set mặc định lúc mở game (VD: Slipper sáng sẵn)
    }

    // 👉 Được gọi từ UnityEvent OnClick của mỗi button trong Inspector
    public virtual void SelectItem(ItemType type)
    {
        // 2. Cập nhật trạng thái sáng/tắt cho TẤT CẢ button — chỉ đúng 1 cái sáng tại 1 thời điểm
        foreach (ButtonSelectItemManager btn in this._Buttons)
        {
            btn.SetSelected(type);
        }
    }
}
