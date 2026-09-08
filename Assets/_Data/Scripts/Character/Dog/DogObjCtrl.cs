using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class DogObjCtrl : ObjCtrl
{
    public DogInputMovement DogInputMovement => this._ObjInputMovement as DogInputMovement;

    [SerializeField] protected DogMovementNavigation _DogMovementNavigation;
    public DogMovementNavigation DogMovementNavigation => this._DogMovementNavigation;

    [SerializeField] protected AIPath _AIPath;
    public AIPath AIPath => this._AIPath;

    [SerializeField] protected DogInputData _dogInputData;
    public DogInputData DogInputData => this._dogInputData;
  
    [SerializeField] protected DogBrain _DogBrain;
    public DogBrain DogBrain => this._DogBrain;

    [SerializeField] protected DogVision _DogVision;
    public DogVision DogVision => this._DogVision;

    [SerializeField] protected DogReceiver _DogReceiver;
    public DogReceiver DogReceiver => this._DogReceiver;

    [SerializeField] protected DogCollisionController _DogCollisionController;
    public DogCollisionController DogCollisionController => this._DogCollisionController;
    public DogModel DogModel => this._ObjModel as DogModel;
   
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadAIPath();
        this.LoadDogInputData();
        this.LoadDogMovementNavigation();
        this.LoadDogBrain();
        this.LoadDogVision();
        this.LoadDogReceiver();
        this.LoadDogCollisionController();
    }

    protected virtual void LoadDogCollisionController()
    {
        if (this._DogCollisionController != null) return;

        this._DogCollisionController = GetComponentInChildren<DogCollisionController>();
    }

    protected virtual void LoadDogReceiver()
    {
        if (this._DogReceiver != null) return;

        this._DogReceiver = GetComponentInChildren<DogReceiver>();
    }

    protected virtual void LoadDogInputData()
    {
        if (this._dogInputData != null) return;

        this._dogInputData = GetComponentInChildren<DogInputData>();
    }

    protected virtual void LoadAIPath()
    {
        if (this._AIPath != null) return;

        this._AIPath = GetComponent<AIPath>();
    }

    protected virtual void LoadDogMovementNavigation()
    {
        if (this._DogMovementNavigation != null) return;

        this._DogMovementNavigation = GetComponentInChildren<DogMovementNavigation>();
    }
    protected virtual void LoadDogBrain()
    {
        if (this._DogBrain != null) return;

        this._DogBrain = GetComponent<DogBrain>();
    }

    protected virtual void LoadDogVision()
    {
        if (this._DogVision != null) return;

        this._DogVision = GetComponentInChildren<DogVision>();
    }

}
