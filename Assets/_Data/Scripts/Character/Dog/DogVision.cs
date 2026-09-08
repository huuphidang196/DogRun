using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogVision : DogCtrlAbstract
{
    [Header("Layer")]
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [Header("View")]
    [SerializeField] private float fovAngle = 180f;

    [Header("Range")]
    [SerializeField] private float inspectionRange = 12f;
    [SerializeField] private float chaseRange = 8f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float senseRange = 3f; // Khoảng cách "cảm nhận" (khứu giác/thính giác) - phát hiện bất kể hướng nhìn. Phải nhỏ hơn chaseRange.

    [SerializeField] private Transform _Vision_Point;

    [SerializeField] private float scanInterval = 0.2f;
    [SerializeField] private float _scanTimer;

  
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadVisionPoint();
    }

    protected virtual void LoadVisionPoint()
    {
        if (this._Vision_Point != null) return;
        this._Vision_Point = transform.Find("VisionPoint");
    }

 
    protected virtual void Update()
    {
        _scanTimer -= Time.deltaTime;

        if (_scanTimer > 0f)
            return;

        _scanTimer = scanInterval;

        ScanVision();
    }

    private void ScanVision()
    {
        Collider2D target =
            Physics2D.OverlapCircle(
                transform.position,
                inspectionRange,
                targetMask);

        if (target == null)
        {
            ClearTarget();
            return;
        }
        CheckTarget(target.transform);
    }
  

    private void CheckTarget(Transform target)
    {
        Vector2 targetPos = target.position;
        Vector2 dir =
            (targetPos - (Vector2)this._Vision_Point.position).normalized;
        float distance =
            Vector2.Distance(
                (Vector2)this._Vision_Point.position,
                targetPos);

        if (!this.CheckScopeFOV(distance, dir)) return;

        RaycastHit2D hit =
            Physics2D.Raycast(
                this._Vision_Point.position,
                dir,
                distance,
                targetMask | obstacleMask);

        // Vẽ tia ray
        Debug.DrawRay(this._Vision_Point.position, dir * distance, Color.red);

        if (hit.collider == null || hit.collider.gameObject.layer != (int)Mathf.Log(targetMask.value, 2))
        {
            ClearTarget();
            return;
        }

        this.DogObjCtrl.DogInputData.SetTarget(target);
        this.DogObjCtrl.DogInputData.LastSeenPosition = target.position;
        UpdateRangeState(distance);
    }

    private bool CheckScopeFOV(float distance, Vector2 dir)
    {
        // 👉 Trong phạm vi "cảm nhận" gần - phát hiện bất kể hướng nhìn (ngửi/nghe thấy)
        if (distance <= senseRange) return true;

        if (this.DogObjCtrl.DogInputData.VisionLevel != VisionLevel.None) return true;

        float angle =
            Vector2.Angle(
                this.DogObjCtrl.DogMovementNavigation.GetFacingDirection(),
                dir);

        if (angle <= fovAngle * 0.5f) return true;

        ClearTarget();
        return false;
    }

    private void UpdateRangeState(float distance)
    {
        if (distance > inspectionRange)
        {
            this.DogObjCtrl.DogInputData.SetVisionLevel(VisionLevel.None);
            return;
        }
        if (distance <= attackRange)
        {
            this.DogObjCtrl.DogInputData.SetVisionLevel(VisionLevel.Attack);
            return;
        }
        if (distance <= chaseRange)
        {
            this.DogObjCtrl.DogInputData.SetVisionLevel(VisionLevel.Chase);
            return;
        }
        this.DogObjCtrl.DogInputData.SetVisionLevel(VisionLevel.Inspection);
    }

    private void ClearTarget()
    {
        this.DogObjCtrl.DogInputData.SetTarget(null);
        this.DogObjCtrl.DogInputData.SetVisionLevel(VisionLevel.None);
        //  this.DogObjCtrl.DogInputData.LastSeenPosition = this._ObjCtrl.transform.position;
    }
}