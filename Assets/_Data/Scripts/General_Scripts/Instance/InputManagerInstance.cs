using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InputManagerInstance : Singleton<InputManagerInstance>
{
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

    public virtual bool CheckInsidePatrolArea(Vector2 obj_NeedChecked)
    {
        if (this.m_PatrolArea == null)
            return false;

        return this.m_PatrolArea.OverlapPoint(obj_NeedChecked);
    }

    public virtual Vector2 GetRandomPointInPatrolArea()
    {
        if (this.m_PatrolArea == null)
            return Vector2.zero;

        Vector2[] points = this.m_PatrolArea.points;

        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 a = points[Random.Range(0, points.Length)];
            Vector2 b = points[Random.Range(0, points.Length)];
            Vector2 c = points[Random.Range(0, points.Length)];

            Vector2 center = (a + b + c) / 3f;

            if (this.CheckInsidePatrolArea(center))
                return center;
        }


        return new Vector2(33f, 30f);

    }

}