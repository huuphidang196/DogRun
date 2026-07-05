using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogIdleState : DogStateAbstract, IDogState
{
    public DogState StateType => DogState.Idle;

    public void Enter()
    {
        this.DogObjCtrl.DogInputMovement.ChangeSpeedData(this.StateType);

        this.DogObjCtrl.DogModel.Play(DogAnimationState.Idle);

        this.DogObjCtrl.DogBrain.SetTimeDurationIdle();
    }

    public void Exit()
    {
        this.DogObjCtrl.DogBrain.ResetWander();
    }

    public void Tick()
    {
        
    }
}
