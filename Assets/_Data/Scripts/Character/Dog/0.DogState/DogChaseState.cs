using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogChaseState : DogStateAbstract, IDogState
{
    public DogState StateType => DogState.Chase;

    public void Enter()
    {
        // dogCtrl.Animation.SetRun();
        this.DogObjCtrl.DogInputMovement.ChangeSpeedData(this.StateType);
        this.DogObjCtrl.DogCollisionController.UpdateCollisionByState(this.StateType); 
        this.DogObjCtrl.DogModel.Play(DogAnimationState.Run);

    }

    public void Tick()
    {
        /*
        if (dogCtrl.InputData.Target == null)
        {
            brain.ChangeState(
                brain.SearchState);

            return;
        }

        dogCtrl.Navigation.MoveTo(
            dogCtrl.InputData.Target.position);
        */
    }

    public void Exit()
    {

    }
}
