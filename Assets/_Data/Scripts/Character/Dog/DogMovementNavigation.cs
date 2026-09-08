using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class DogMovementNavigation : ObjMovement
{
    public DogObjCtrl DogObjCtrl => this._ObjCtrl as DogObjCtrl;

    protected override void FixedUpdate()
    {
        this.DogObjCtrl.AIPath.maxSpeed = this.DogObjCtrl.DogInputMovement.OutputMovement().x;
        this.DogObjCtrl.AIPath.destination = this.DogObjCtrl.DogInputData.LastSeenPosition;

    }

    public Vector2 GetFacingDirection()
    {
        float sign = Mathf.Sign(this.DogObjCtrl.DogModel.transform.localScale.x);
        return new Vector2(sign, 0f);
    }

}
