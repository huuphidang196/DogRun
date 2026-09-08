using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPatrolState : DogStateAbstract, IDogState
{
    public DogState StateType => DogState.Patrol;

    public void Enter()
    {
        this.DogObjCtrl.DogInputMovement.ChangeSpeedData(this.StateType);

        this.DogObjCtrl.DogCollisionController.UpdateCollisionByState(this.StateType);

        this.DogObjCtrl.DogModel.Play(DogAnimationState.Walk);

        this.DogObjCtrl.DogInputData.LastSeenPosition = InputManagerInstance.Instance.GetRandomPointInPatrolArea();
    }

    public void Tick()
    {
        /*
        if (dogCtrl.InputData.IsAttackRange)
        {
            brain.ChangeState(
                brain.AttackState);

            return;
        }

        if (dogCtrl.InputData.IsChaseRange)
        {
            brain.ChangeState(
                brain.ChaseState);

            return;
        }

        if (dogCtrl.Navigation.HasReachedDestination())
        {
            dogCtrl.Navigation.MoveRandomPoint();
        }
        */
    }

    public void Exit()
    {

    }

   
}
