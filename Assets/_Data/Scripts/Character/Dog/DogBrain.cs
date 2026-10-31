using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DogBrain : DogCtrlAbstract
{
    [SerializeField] protected List<IDogState> _States = new List<IDogState>();

    [SerializeField] private IDogState currentState;
    [SerializeField] protected DogState DogState;// test
    #region Wander

    [SerializeField] private float minIdleTime = 0.5f;
    [SerializeField] private float maxIdleTime = 5f;
    public float MinIdleTime => minIdleTime;
    public float MaxIdleTime => maxIdleTime;

    private float _wanderTimer_State;
    public float WanderTimer => _wanderTimer_State;

    private float _wanderDuration_Idle;
    public float WanderDuration => this._wanderDuration_Idle;

    #endregion

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStatesEnum();
    }

    protected virtual void LoadStatesEnum()
    {
        if (this._States.Count > 0) return;

        this._States.Clear();

        this._States.AddRange(GetComponentsInChildren<IDogState>(true));
        Debug.Log("State: " + this._States.Count);
    }

    protected override void LoadObjCtrl()
    {
        if (this._ObjCtrl != null) return;

        this._ObjCtrl = transform.GetComponent<ObjCtrl>();
    }
    public void ChangeState(IDogState newState)
    {
        if (newState == currentState || newState == null) return;

        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
        this.DogState = currentState.StateType;

        //Debug.Log("IDog: " + currentState);
    }
    public virtual void ResetWander()
    {
        _wanderTimer_State = 0f;
        _wanderDuration_Idle = 0f;
    }
    public virtual void SetTimeDurationIdle()
    {
        this._wanderDuration_Idle = Random.Range(minIdleTime, maxIdleTime);
    }

    private void Update()
    {
        this.ProcessState();

        currentState?.Tick();
    }

    protected virtual void ProcessState()
    {
        // Ưu tiên 1: Stun
        if (this.DogObjCtrl.DogReceiver.IsStun)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Stun));
            this.ProcessStateStun();
            return;
        }

        Vector3 pos = this.DogObjCtrl.transform.position;

        // Ưu tiên 2: Return Home nếu Y < -2f
        if (pos.y < -2f)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.ReturnHome));
            return;
        }

        // Ưu tiên 3: Attack
        if (this.DogObjCtrl.DogInputData.VisionLevel == VisionLevel.Attack)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Attack));
            return;
        }

        // Ưu tiên 4: Chase
        if (this.DogObjCtrl.DogInputData.VisionLevel == VisionLevel.Chase)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Chase));
            return;
        }

        // Ưu tiên 5: Inspection
        if (this.DogObjCtrl.DogInputData.VisionLevel == VisionLevel.Inspection)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Inspect));
            return;
        }

        // Return Home nếu không có target và ra khỏi Patrol Area
        if (!this.DogObjCtrl.DogInputData.HasTarget &&
            !this.DogObjCtrl.DogInputMovement.CheckInsidePatrolArea())
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.ReturnHome));
            return;
        }

        // Patrol
        if (!this.DogObjCtrl.AIPath.reachedDestination &&
            this.DogObjCtrl.DogInputMovement.CheckInsidePatrolArea())
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Patrol));
            return;
        }

        // Idle
        this.ChangeState(this._States.Find(x => x.StateType == DogState.Idle));
        this.ProcessStateIdle();
    }
    protected virtual void ProcessStateIdle()
    {
        _wanderTimer_State += Time.deltaTime;

        if (_wanderTimer_State >= _wanderDuration_Idle)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Patrol));
        }
    }
    protected virtual void ProcessStateStun()
    {
        _wanderTimer_State += Time.deltaTime;

        if (_wanderTimer_State <= this.DogObjCtrl.DogInputData.Time_Stun) return;

        this.DogObjCtrl.DogReceiver.ExitStunState();
    }

}
public enum DogState
{
    Idle,
    Patrol,
    Inspect,
    Chase,
    Attack,
    ReturnHome,
    Stun
}
public enum VisionLevel
{
    None,

    Inspection, //diff with patrol since move on target

    Chase,

    Attack
}
public interface IDogState
{
    DogState StateType { get; }

    void Enter();

    void Tick();

    void Exit();
}