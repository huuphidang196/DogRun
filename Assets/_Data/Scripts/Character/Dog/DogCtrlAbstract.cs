using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCtrlAbstract : ObjCtrlAbstract
{
    public DogObjCtrl DogObjCtrl => this._ObjCtrl as DogObjCtrl;

    
}
