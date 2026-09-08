using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowableInputMovement : ObjInputMovement
{
    [SerializeField] protected float _Speed_Move;
    [SerializeField] protected Vector2 _Target_Pos;

    public virtual void SetSpeedThrowOverall(float speed) => this._Speed_Move = speed;
    public virtual void SetTargetFollow(Vector2 Pos_des) => this._Target_Pos = Pos_des;

    public override Vector2 OutputMovement()
    {
        if (this._ObjCtrl == null) return Vector2.zero;

        Vector2 direction = this._Target_Pos - (Vector2)this._ObjCtrl.transform.position;
        float distance = direction.magnitude;

        // Đã đến đủ gần target thì dừng lại, tránh rung lắc
        if (distance < 0.1f) // ngưỡng tuỳ chỉnh theo scale game
        {
            this._Output_Velocity = Vector2.zero;
            return this._Output_Velocity;
        }

        this._Output_Velocity = direction.normalized * this._Speed_Move;
        return this._Output_Velocity;
    }
}
