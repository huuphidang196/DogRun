using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjModel : ObjCtrlAbstract
{
    [SerializeField] protected Animator _Char_Animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadCharaterAnimator();
    }

    protected virtual void LoadCharaterAnimator()
    {
        if (this._Char_Animator != null) return;

        this._Char_Animator = GetComponent<Animator>();
    }
}
