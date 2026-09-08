using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogReturnHomeState : DogStateAbstract, IDogState
{
    public DogState StateType => DogState.ReturnHome;

    public void Enter()
    {
        this.DogObjCtrl.DogInputMovement.ChangeSpeedData(this.StateType);

        this.DogObjCtrl.DogCollisionController.UpdateCollisionByState(this.StateType);

        this.DogObjCtrl.DogModel.Play(DogAnimationState.Walk);

        this.DogObjCtrl.DogInputData.LastSeenPosition =
           0.5f * (InputManagerInstance.Instance.PatrolArea.points[1] + InputManagerInstance.Instance.PatrolArea.points[2]) + new Vector2(0, 2);
        
    }

    public void Tick()
    {

    }

    public void Exit()
    {

    }
}
