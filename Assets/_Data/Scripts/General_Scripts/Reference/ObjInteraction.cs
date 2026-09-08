using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjInteraction : ObjCtrlAbstract
{
    [SerializeField]
    protected LayerMask targetMask;

    protected bool IsTarget(GameObject obj)
    {
        return ((1 << obj.layer) & targetMask) != 0;
    }

    public abstract void Interact(GameObject target);
}
