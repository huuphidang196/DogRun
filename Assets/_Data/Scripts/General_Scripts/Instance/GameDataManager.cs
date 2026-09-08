using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    [SerializeField] protected Vector2 _Player_Direction;

    public Vector2 Player_Direction => _Player_Direction;

    [SerializeField] protected bool isThrowInput = false;

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
            case TimeActiveButtonRight.Throw_Button:
                return 3f;
        }
        return 0;
    }

    #endregion
}
