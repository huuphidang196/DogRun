using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveJoyStick : CanvasCameraBehaviour
{
    //[SerializeField] protected bool active = false;
    [SerializeField] Image _imgBGJoy;
    [SerializeField] Image _imgHandleJoy;

    [SerializeField] protected FixedJoystick _joystick;

    [SerializeField] private int activeFingerId = -1;
    [SerializeField] private bool isHolding = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageBackGroundJoyStick();
        this.LoadImageHandleJoyStick();
        this.LoadFixedJoystick();
    }

    protected virtual void LoadFixedJoystick()
    {
        if (this._joystick != null) return;

        this._joystick = GetComponent<FixedJoystick>();
    }

    protected virtual void LoadImageBackGroundJoyStick()
    {
        if (this._imgBGJoy != null) return;
        this._imgBGJoy = GetComponent<Image>();
    }

    protected virtual void LoadImageHandleJoyStick()
    {
        if (this._imgHandleJoy != null) return;
        this._imgHandleJoy = transform.GetChild(0).GetComponent<Image>();
    }

    protected virtual void Update()
    {
        if (Input.touchCount == 0) return;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // 👉 Nếu đang giữ joystick → chỉ xử lý đúng finger đó
            if (isHolding)
            {
                if (touch.fingerId != activeFingerId)
                    continue;

                HandleHoldingTouch(touch);
                break; // chỉ cần xử lý 1 touch
            }

            // 👉 Chưa giữ → chỉ quan tâm Began
            if (touch.phase != TouchPhase.Began)
                continue;

            // ❌ bỏ qua UI (chỉ check lúc bắt đầu)
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                continue;

            // 👉 Convert khi cần
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                touch.position,
                _canvas.worldCamera,
                out localPos
            );

            // 👉 chỉ bên trái
            if (localPos.x > 0)
                continue;

            // 🟢 BEGIN
            this.ShowHideUIJoyStick(true);

            activeFingerId = touch.fingerId;
            isHolding = true;

            _joystick.Show(localPos);
        }
    }
    protected virtual void HandleHoldingTouch(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                _joystick.Drag(touch.position);
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                {
                    activeFingerId = -1;
                    isHolding = false;
                    _joystick.Hide();
                    this.ShowHideUIJoyStick(false);
                }
                break;
        }
    }

    protected virtual void ShowHideUIJoyStick(bool isShowed)
    {
        _imgBGJoy.enabled = isShowed;
        _imgHandleJoy.enabled = isShowed;

        _imgBGJoy.raycastTarget = isShowed;
        _imgHandleJoy.raycastTarget = isShowed;
    }

}
