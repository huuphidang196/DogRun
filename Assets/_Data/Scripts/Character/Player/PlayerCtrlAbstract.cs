using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlAbstract : ObjCtrlAbstract
{
    public PlayerCtrl PlayerCtrl => this._ObjCtrl as PlayerCtrl;
}
