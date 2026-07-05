using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraBehaviour : StandMonoBehaviour
{
    [SerializeField] protected Canvas _canvas;
    [SerializeField] protected Camera m_cam;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvasJoyStick();
    }
    protected virtual void LoadCanvasJoyStick()
    {
        if (this._canvas != null) return;

        this._canvas = GetComponentInParent<Canvas>();
    }

    protected override void Start()
    {
        base.Start();

        this.m_cam = (_canvas.renderMode == RenderMode.ScreenSpaceCamera) ? _canvas.worldCamera : null;
    }

}
