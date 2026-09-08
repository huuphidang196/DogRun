using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogStunState : DogStateAbstract, IDogState
{
    public DogState StateType => DogState.Stun;

    public void Enter()
    {
        this.DogObjCtrl.DogInputMovement.ChangeSpeedData(this.StateType);

        this.DogObjCtrl.DogCollisionController.UpdateCollisionByState(this.StateType);

        this.DogObjCtrl.DogBrain.ResetWander();

        this.DogObjCtrl.DogModel.Play(DogAnimationState.Stun);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }  
}
