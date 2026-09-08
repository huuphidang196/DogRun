using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogModel : ObjMovementModel
{
    public DogObjCtrl DogObjCtrl => this._ObjCtrl as DogObjCtrl;

   [SerializeField] private static readonly int AnimStateHash =
          Animator.StringToHash("AnimState");

    [SerializeField]
    private DogAnimationState currentAnim =
        DogAnimationState.Idle;

    [SerializeField] protected SpriteRenderer _SP;
    public SpriteRenderer SpriteRenderer => this._SP;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadSpriteRenderer();
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this._SP == null) return;

        this._SP = GetComponent<SpriteRenderer>();
    }

    public void Play(DogAnimationState state)
    {
        if (currentAnim == state)
            return;

        currentAnim = state;

        this._Char_Animator.SetInteger(AnimStateHash, (int)state);
    }

    protected override void GetInputSpeedMovement()
    {
        this._moveInput = this.DogObjCtrl.AIPath.desiredVelocity;
    }
    protected override void Update()
    {
        base.Update();
        this.ProcessFlipModel();
    }

    protected override void MoreProgressFlip()
    {
        //Flip with VisionPoint
        this.DogObjCtrl.DogVision.transform.localScale = transform.localScale;
    }
}
public enum DogAnimationState
{
    Idle = 0,
    Walk = 1,
    Run = 2,
    Attack = 3,
    Stun = 4
}