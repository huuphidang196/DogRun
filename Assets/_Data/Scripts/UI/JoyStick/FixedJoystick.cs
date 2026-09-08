using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : CanvasCameraBehaviour
{
    private Vector2 input = Vector2.zero;
    public Vector2 Direction => this.input;

    [Range(0f, 1f)]
    [SerializeField] private float handleRange = 0.35f;
    [SerializeField] private float maxRadius;

    [SerializeField] private RectTransform background = null;
    [SerializeField] private RectTransform handle = null;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRectTransformBackGroundJoyStick();
        this.LoadHandleJoyStick();
    }

    protected virtual void LoadHandleJoyStick()
    {
        if (this.handle != null) return;
        this.handle = this.transform.GetChild(0).GetComponent<RectTransform>();

        Vector2 center = new Vector2(0.5f, 0.5f);
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    protected virtual void LoadRectTransformBackGroundJoyStick()
    {
        if (this.background != null) return;
        this.background = GetComponent<RectTransform>();

        Vector2 center = new Vector2(0.5f, 0.5f);
        this.background.pivot = center;
    }

    protected override void Start()
    {
        base.Start();

        this.maxRadius = this.background.sizeDelta.x / 2f;
        this.Hide();
    }

    protected virtual void Update()
    {
        GameDataManager.Instance.SetDirection(this.input);
    }
    public void Show(Vector2 anchoredPos)
    {
        this.background.anchoredPosition = anchoredPos;
    }

    public void Hide()
    {
        this.input = Vector2.zero;
        this.handle.anchoredPosition = Vector3.zero;
    }

    public void Drag(Vector2 screenPos)
    {
        if (this.m_cam == null) return;

        Vector2 center = RectTransformUtility.WorldToScreenPoint(this.m_cam, background.position);
        Vector2 delta = screenPos - center;

        float radius = maxRadius * handleRange;

        // 👉 1. Clamp để lấy vị trí trong vòng tròn
        Vector2 clamped = delta.magnitude <= radius ? delta : delta.normalized * radius;

        // 👉 2. INPUT (logic) → normalize về [-1,1]
        //this.input = clamped / radius;
        this.input = delta.normalized;

        // 👉 3. HANDLE (UI) → smooth tới vị trí
        this.handle.anchoredPosition = Vector2.Lerp(
            this.handle.anchoredPosition,
            clamped,
            Time.deltaTime * 20f
        );

    }

}
public enum AxisOptions { Both, Horizontal, Vertical }