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

    public event Action OnThrowStart;
    public event Action OnThrowAnimationEnd;
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
        if (!_isThrowing && GameDataManager.Instance.IsPlayerThrow())
        {
            _isThrowing = true;
            this.OnThrowStart?.Invoke(); // 👉 nơi này sẽ trigger việc quay mặt về chó gần n
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
                GameDataManager.Instance.SetPlayerThrowInput(false);
                this.OnThrowAnimationEnd?.Invoke();
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
    // 👉 Override: khi đang ném thì KHÔNG cho movement tự flip nữa (giữ hướng đã quay về chó)
    protected override void ProcessFlipModel()
    {
        if (_isThrowing) return;
        base.ProcessFlipModel();
    }
    // 👉 Hàm mới: quay mặt theo 1 vị trí target bất kỳ (dùng cho throw action)
    // Dùng lại đúng cơ chế ApplyFlip() của class cha, đảm bảo _currentDir và MoreProgressFlip() luôn đồng bộ
    public virtual void FaceTarget(Vector2 targetPos)
    {
        float deltaX = targetPos.x - transform.position.x;
        if (Mathf.Approximately(deltaX, 0f)) return;

        int dir = deltaX > 0 ? 1 : -1;
        this.ApplyFlip(dir); // gọi lại đúng hàm chung ở class cha, không viết code set localScale trùng lặp
    }
}
