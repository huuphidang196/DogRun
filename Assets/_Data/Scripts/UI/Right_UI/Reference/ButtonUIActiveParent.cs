using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUIActiveParent : StandMonoBehaviour
{
    [SerializeField] protected ActiveUIButton _ActiveUIButton;

    [SerializeField] protected ButtonUIStandard _ButtonUIStandard;
    public ButtonUIStandard ButtonUIStandard => _ButtonUIStandard;

    [SerializeField] protected TimeActiveButtonRight _TypeButtonActive;
    public TimeActiveButtonRight TypeButtonActive => _TypeButtonActive;

    protected override void ResetValue()
    {
        base.ResetValue();

        this.LoadTypeButtonActive();
    }

    protected virtual void LoadTypeButtonActive()
    {
        if (this._ButtonUIStandard == null) return;

        string objName = this._ButtonUIStandard.gameObject.name;

        if (Enum.TryParse(objName, out TimeActiveButtonRight result))
        {
            this._TypeButtonActive = result;
        }
        else
        {
            this._TypeButtonActive = TimeActiveButtonRight.NoType;
            Debug.LogWarning($"Không tìm thấy enum TimeActiveButtonRight khớp với tên '{objName}' trên object {this.name}");
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadActiveUIButton();
        this.LoadButtonUIStandard();
    }

    protected virtual void LoadButtonUIStandard()
    {
        if (this._ButtonUIStandard != null) return;

        this._ButtonUIStandard = GetComponentInChildren<ButtonUIStandard>();
    }

    protected virtual void LoadActiveUIButton()
    {
        if (this._ActiveUIButton != null) return;

        this._ActiveUIButton = GetComponentInChildren<ActiveUIButton>();
    }
}

public enum TimeActiveButtonRight
{
    NoType = 0,

    Throw_Button = 1,

}