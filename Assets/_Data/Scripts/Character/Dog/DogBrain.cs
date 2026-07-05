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

    private float _wanderTimer_Idle;
    public float WanderTimer => _wanderTimer_Idle;

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
    }

    public void ChangeState(IDogState newState)
    {
        if (newState == currentState) return;

        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
        this.DogState = currentState.StateType;
    }
    public virtual void ResetWander()
    {
        _wanderTimer_Idle = 0f;
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
        if (this.DogObjCtrl.DogInputData.VisionLevel == VisionLevel.Attack)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Attack));
            return;
        }
        if (this.DogObjCtrl.DogInputData.VisionLevel == VisionLevel.Chase)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Chase));
            return;
        }
        if (this.DogObjCtrl.DogInputData.VisionLevel == VisionLevel.Inspection)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Inspect));
            return;
        }
        //None => 4 case : Idle, patrol, stun, return home
        //return home
        if (!this.DogObjCtrl.DogInputData.HasTarget && !this.DogObjCtrl.DogInputMovement.CheckInsidePatrolArea())
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.ReturnHome));
            return;
        }

        if (!this.DogObjCtrl.AIPath.reachedDestination && this.DogObjCtrl.DogInputMovement.CheckInsidePatrolArea())
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Patrol));
            return;
        }

        this.ChangeState(this._States.Find(x => x.StateType == DogState.Idle));
        this.ProcessStateIdle();
    }

    protected virtual void ProcessStateIdle()
    {
        _wanderTimer_Idle += Time.deltaTime;

        if (_wanderTimer_Idle >= _wanderDuration_Idle)
        {
            this.ChangeState(this._States.Find(x => x.StateType == DogState.Patrol));
        }
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