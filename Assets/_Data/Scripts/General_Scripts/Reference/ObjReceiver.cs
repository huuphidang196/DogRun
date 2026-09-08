using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjReceiver : ObjCtrlAbstract
{
    [Header("Receiver")]
    [SerializeField]
    protected LayerMask interactionMask;

    protected virtual bool CanReceive(GameObject sender)
    {
        return ((1 << sender.layer) & interactionMask) != 0;
    }

    /// <summary>
    /// Nhận tương tác.
    /// </summary>
    public void Receive(ObjInteraction sender)
    {
        if (sender == null)
            return;

        if (!CanReceive(sender.gameObject))
            return;

        OnReceive(sender);
    }

    /// <summary>
    /// Lớp con xử lý.
    /// </summary>
    protected abstract void OnReceive(ObjInteraction sender);
}
