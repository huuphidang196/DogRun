using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCollisionController : DogCtrlAbstract
{
    protected int LAYER_DOG_PASSIVE;
    protected int LAYER_DOG_SOLID;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadLayerDog();
    }

    protected virtual void LoadLayerDog()
    {
        LAYER_DOG_PASSIVE = LayerMask.NameToLayer("Dog_Passive");
        LAYER_DOG_SOLID = LayerMask.NameToLayer("Dog_Solid");
    }


    public virtual void UpdateCollisionByState(DogState dogState)
    {
        switch (dogState)
        {
            case DogState.Patrol:
            case DogState.Inspect:
            case DogState.ReturnHome:
            case DogState.Stun:
                this.SetLayer(LAYER_DOG_PASSIVE);
                break;

            case DogState.Chase:
            case DogState.Attack:
                this.SetLayer(LAYER_DOG_SOLID);
                break;

            default:
                break;
        }
    }

    protected virtual void SetLayer(int layer)
    {
        this.DogObjCtrl.gameObject.layer = layer;
    }
}
