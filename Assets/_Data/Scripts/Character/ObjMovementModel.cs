using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMovementModel : ObjModel
{
    [SerializeField] protected Vector2 _moveInput;

    [SerializeField] protected int _currentDir = 1;

    protected virtual void Update()
    {
        this.GetInputSpeedMovement();
    }

    // ================= INPUT =================
    protected virtual void GetInputSpeedMovement()
    {
        _moveInput = this._ObjCtrl.ObjMovement.Rigidbody2D.velocity;
    }

    // ================= FLIP =================
    protected virtual void ProcessFlipModel()
    {
        if (Mathf.Abs(_moveInput.x) < 0.1f) return;

        int dir = _moveInput.x > 0 ? 1 : -1;

        if (dir != _currentDir)
        {
            _currentDir = dir;
        
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(scale.x) * dir, scale.y,0);
          //  Debug.Log("scale: " + scale);
        }
    }
}
