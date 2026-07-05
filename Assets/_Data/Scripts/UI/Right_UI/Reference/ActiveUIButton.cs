using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveUIButton : StandMonoBehaviour
{
    [SerializeField] protected ButtonUIActiveParent _ButtonUIActiveParent;
    [SerializeField] protected Image _img_UI_Cooldown_Active;
    [SerializeField] protected float _cooldownTime = 5f;

    protected float currentTime = 0f;
    protected bool isCoolingDown = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadButtonUIActiveParent();
        this.LoadImageUIButtonActive();
    }

    protected virtual void LoadImageUIButtonActive()
    {
        if (this._img_UI_Cooldown_Active != null) return;

        this._img_UI_Cooldown_Active = GetComponent<Image>();
    }

    protected virtual void LoadButtonUIActiveParent()
    {
        if (this._ButtonUIActiveParent != null) return;

        this._ButtonUIActiveParent = transform.parent.GetComponent<ButtonUIActiveParent>();
    }

    protected virtual void Update()
    {
        this.CheckStartCooldown(); // 👉 để class con tự quyết định

        if (isCoolingDown)
        {
            this.ProcessCooldown();
        }
    }

    // 👉 Class con override để quyết định khi nào bắt đầu
    protected virtual void CheckStartCooldown()
    {
        // Ví dụ: để trống, class con sẽ override
    }

    protected virtual void StartCooldown()
    {
        isCoolingDown = true;
        currentTime = _cooldownTime;

        if (_img_UI_Cooldown_Active != null)
        {
            this._img_UI_Cooldown_Active.enabled = true;

            //inactive button
            this._ButtonUIActiveParent.ButtonUIStandard.BtnExect.interactable = false;

            _img_UI_Cooldown_Active.fillAmount = 1f;
        }
    }

    protected virtual void ProcessCooldown()
    {
        currentTime -= Time.deltaTime;

        if (_img_UI_Cooldown_Active != null)
        {
            _img_UI_Cooldown_Active.fillAmount = currentTime / _cooldownTime;
        }

        if (currentTime <= 0f)
        {
            this.EndCooldown();
        }
    }

    protected virtual void EndCooldown()
    {
        isCoolingDown = false;
        currentTime = 0f;

        if (_img_UI_Cooldown_Active != null)
        {
            _img_UI_Cooldown_Active.fillAmount = 0f;
            this._img_UI_Cooldown_Active.enabled = false;
            //active button
            this._ButtonUIActiveParent.ButtonUIStandard.BtnExect.interactable = true;
        }
    }
}

