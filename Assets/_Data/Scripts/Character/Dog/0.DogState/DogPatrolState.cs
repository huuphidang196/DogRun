using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPatrolState : DogStateAbstract, IDogState
{
    public DogState StateType => DogState.Patrol;

    public void Enter()
    {
        this.DogObjCtrl.DogInputMovement.ChangeSpeedData(this.StateType);

        this.DogObjCtrl.DogModel.Play(DogAnimationState.Walk);

        this.DogObjCtrl.DogInputData.LastSeenPosition = this.GetRandomPointInPatrolArea();
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

    private Vector2 GetRandomPointInPatrolArea()
    {
        Bounds bounds = InputManagerInstance.Instance.PatrolArea.bounds;

        float randomX = Random.Range(bounds.min.x + 1f, bounds.max.x - 1f);
        float randomY = Random.Range(bounds.min.y + 1f, bounds.max.y - 1f);

        return new Vector2(randomX, randomY);
    }
}
