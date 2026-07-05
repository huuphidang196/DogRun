using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjInputMovement : ObjCtrlAbstract
{
    [SerializeField] protected Vector2 _Output_Velocity;
    public Vector2 Output_Velocity => this._Output_Velocity;

    public abstract Vector2 OutputMovement();


}
