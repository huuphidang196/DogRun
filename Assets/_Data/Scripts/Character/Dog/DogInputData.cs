using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInputData : StandMonoBehaviour
{
    [Header("Target")]
    [SerializeField] protected Transform _target;
    public Transform Target => this._target;

    public bool HasTarget => this._target != null;

    [Header("Distance")]
    [SerializeField] protected VisionLevel _VisionLevel = VisionLevel.None;
    public VisionLevel VisionLevel => this._VisionLevel;

    [Header("Last Seen")]

    public Vector2 LastSeenPosition;

    public virtual void SetTarget(Transform target) => this._target = target;

    public virtual void SetVisionLevel(VisionLevel visionLevel) => this._VisionLevel = visionLevel;

}
