using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMovement : ObjInputMovement
{
    [SerializeField] protected float _Speed = 5f;

    public override Vector2 OutputMovement()
    {
        this._Output_Velocity = InputManagerInstance.Instance.IsPlayerThrow() ? Vector2.zero : InputManagerInstance.Instance.Player_Direction;
        return this._Output_Velocity * this._Speed;
    }
}
