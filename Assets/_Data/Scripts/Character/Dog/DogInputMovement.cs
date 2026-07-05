using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInputMovement : ObjInputMovement
{
    //Read data speed from scriptable_Obj
    public DogObjCtrl DogObjCtrl => this._ObjCtrl as DogObjCtrl;

    [SerializeField] protected float _Speed_Chase = 5f; //Run
    public float Speed_Chase => this._Speed_Chase;

    [SerializeField] protected float _Speed_Inspection = 2f; //Walk

    [SerializeField] protected float _Speed_Patrol_Inspection = 1f; //Walk
    public float Speed_Patrol_Inspection => this._Speed_Patrol_Inspection;

    public virtual bool CheckInsidePatrolArea()
    {
        return InputManagerInstance.Instance.CheckInsidePatrolArea(this.DogObjCtrl.transform.position);
    }

    public virtual void ChangeSpeedData(DogState dogState)
    {
        switch (dogState)
        {
            case DogState.Idle:
            case DogState.Attack:
            case DogState.Stun:
                this._Output_Velocity = Vector2.zero;
                break;
            case DogState.Patrol:
                this._Output_Velocity = new Vector2(this._Speed_Patrol_Inspection, 0);
                break;
            case DogState.Inspect:       
            case DogState.ReturnHome:
                this._Output_Velocity = new Vector2(this._Speed_Inspection, 0);
                break;
            case DogState.Chase:
                this._Output_Velocity = new Vector2(this._Speed_Chase, 0);
                break;
            default:
                break;
        }
    }

    public override Vector2 OutputMovement()
    {
        return this._Output_Velocity;// * this.DogObjCtrl.DogMovementNavigation.Rigidbody2D.velocity;
    }


}
