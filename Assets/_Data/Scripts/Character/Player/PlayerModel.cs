using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : ObjMovementModel
{
    [SerializeField] protected bool isRun = false;
    [SerializeField] protected bool isThrow = false;

   
    [SerializeField] protected bool _isThrowing = false;
    public bool IsThrowing => this._isThrowing;

    protected override void LoadCharaterAnimator()
    {
        if (this._Char_Animator != null) return;

        this._Char_Animator = transform.Find("Char_Model").GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();

        this.ProcessThrowState();
        this.ProcessAnimation();
        this.ProcessFlipModel();
    }

    // ================= THROW =================
    protected virtual void ProcessThrowState()
    {
        // 👉 Nhận input 1 lần
        if (!_isThrowing && InputManagerInstance.Instance.IsPlayerThrow())
        {
            _isThrowing = true;
        }

        if (_isThrowing)
        {
            AnimatorStateInfo state = this._Char_Animator.GetCurrentAnimatorStateInfo(0);

            // 👉 CHỈ check khi đang ở animation Throw
            if (!this._Char_Animator.IsInTransition(0) &&
                state.IsTag("Throw") &&
                state.normalizedTime >= 1f)
            {
                _isThrowing = false;
                InputManagerInstance.Instance.SetPlayerThrowInput(false);
            }
        }

        isThrow = _isThrowing;
    }

    // ================= ANIMATION =================
    protected virtual void ProcessAnimation()
    {
        isRun = !_isThrowing && _moveInput != Vector2.zero;

        this._Char_Animator.SetBool("Run", isRun);
        this._Char_Animator.SetBool("Throw", isThrow);
    }

}
