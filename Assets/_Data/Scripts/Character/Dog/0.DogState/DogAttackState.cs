using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAttackState : DogStateAbstract, IDogState
{
    public DogState StateType => DogState.Attack;

    public void Enter()
    {      
        this.DogObjCtrl.DogInputMovement.ChangeSpeedData(this.StateType);

        this.DogObjCtrl.DogCollisionController.UpdateCollisionByState(this.StateType);

        this.DogObjCtrl.DogModel.Play(DogAnimationState.Attack);
    }

    public void Tick()
    {
        /*
        if (!dogCtrl.InputData.IsAttackRange)
        {
            brain.ChangeState(
                brain.ChaseState);
        }
        */
    }

    public void Exit()
    {

    }
}
