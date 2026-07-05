using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerInstance : Singleton<InputManagerInstance>
{
    [SerializeField] protected Vector2 _Player_Direction;

    public Vector2 Player_Direction => _Player_Direction;

    [SerializeField] protected bool isThrowInput = false;

    [SerializeField]
    private PolygonCollider2D m_PatrolArea;
    public PolygonCollider2D PatrolArea => this.m_PatrolArea;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadPatrolArea();
    }

    private void LoadPatrolArea()
    {
        if (this.m_PatrolArea != null) return;

        this.m_PatrolArea = transform.Find("Patrol_Area").GetComponent<PolygonCollider2D>();
    }

    public virtual void SetDirection(Vector2 dir)
    {
        this._Player_Direction = dir;
    }


    #region OutSideReference
    public bool IsPlayerThrow()
    {
        return this.isThrowInput;
    }
    public virtual void SetPlayerThrowInput(bool isThrow)
    {
        this.isThrowInput = isThrow;
    }

    public virtual float GetCooldownTimeByTypeButtonActive(TimeActiveButtonRight typeButtonActive)
    {
        switch (typeButtonActive)
        {     
            case TimeActiveButtonRight.Button_Throw:
                return 3f;
        }
        return 0;
    }

    public virtual bool CheckInsidePatrolArea(Vector2 obj_NeedChecked)
    {
        return this.m_PatrolArea.bounds.Contains(obj_NeedChecked);
    }
    #endregion
}