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
        return (this._Rig2D.velocity.sqrMagnitude > 0.01f) ? this._Rig2D.velocity.normalized : this.DogObjCtrl.transform.right;
    }

}
