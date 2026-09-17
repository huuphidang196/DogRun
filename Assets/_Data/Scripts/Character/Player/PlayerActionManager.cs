using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : PlayerCtrlAbstract
{
    [SerializeField] protected List<IPlayerAction> _Actions = new List<IPlayerAction>();
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadIPlayerActions();
    }

    protected virtual void LoadIPlayerActions()
    {
        if (this._Actions.Count > 0) return;

        this._Actions.Clear();

        this._Actions.AddRange(GetComponentsInChildren<IPlayerAction>(true));
        Debug.Log(this._Actions[0].PlayerActionType.ToString());
    }

    public void Execute(PlayerActionType typeAction)
    {
        IPlayerAction action = this._Actions.Find(x => x.PlayerActionType == typeAction);
        if (action.CanExecute())
            action.Execute();
    }

    protected virtual void Update()
    {
        this.transform.localScale = this.PlayerCtrl.PlayerModel.transform.localScale;

        this.ProcessPlayerAction();
    }

    protected virtual void ProcessPlayerAction()
    {
        if (GameDataManager.Instance.IsPlayerThrow()) this.Execute(PlayerActionType.Throw);

    }
    public virtual void UserWantToChangeStateCurrentPlot() => this.Execute(PlayerActionType.Plant);
}
public enum PlayerActionType
{
    NoAction,

    Throw,
    Plant,
    Fishing
}

public interface IPlayerAction
{
    public PlayerActionType PlayerActionType { get; }

    public bool CanExecute();

    public void Execute();
}