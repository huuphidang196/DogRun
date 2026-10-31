using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogReceiver : ObjReceiver
{
    public DogObjCtrl DogCtrl => this._ObjCtrl as DogObjCtrl;

    [SerializeField]
    protected bool isStun = false;
    public bool IsStun => this.isStun;

    protected override void OnReceive(ObjInteraction sender)
    {
        if (this.isStun) return;

        //Banana. Slipper
        if (this.IsItemThrowable(sender)) return;
    }

    private bool IsItemThrowable(ObjInteraction sender)
    {
        ItemThrowableInteraction item = sender as ItemThrowableInteraction;

        if (item == null) return false;

        this.isStun = true;

        //Set false after timeDelay
        Debug.Log("Stun");

        return true;
        
    }

    public virtual void ExitStunState() => this.isStun = false;
}
