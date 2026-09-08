using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowItemAction : PlayerActionManagerAbstract, IPlayerAction
{
    public PlayerActionType PlayerActionType => PlayerActionType.Throw;

    [SerializeField] protected Transform _Point_Throw;

    [SerializeField] protected float _Speed_Throw = 15f;
    [SerializeField] protected bool isThrown = true;
    protected override void OnEnable()
    {
        base.OnEnable();

        if (this._PlayerActionManager.PlayerCtrl.PlayerModel != null)
        {
            this._PlayerActionManager.PlayerCtrl.PlayerModel.OnThrowStart += this.HandleThrowStart;
            this._PlayerActionManager.PlayerCtrl.PlayerModel.OnThrowAnimationEnd += this.OnThrowAnimationJustFinished;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (this._PlayerActionManager.PlayerCtrl.PlayerModel != null)
        {
            this._PlayerActionManager.PlayerCtrl.PlayerModel.OnThrowStart -= this.HandleThrowStart;
            this._PlayerActionManager.PlayerCtrl.PlayerModel.OnThrowAnimationEnd -= this.OnThrowAnimationJustFinished;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadPointThrow();
    }

    protected virtual void LoadPointThrow()
    {
        if (this._Point_Throw != null) return;

        this._Point_Throw = transform.Find("Point_Throw");
    }
    protected virtual void HandleThrowStart()
    {
        Transform nearestDog = DogSpawner.Instance.GetDogNearestPlayer();
        if (nearestDog != null)
        {
            this._PlayerActionManager.PlayerCtrl.PlayerModel.FaceTarget(nearestDog.position); // 👉 dùng hàm mới, nhất quán với cơ chế flip gốc
        }
    }
    public bool CanExecute()
    {
        return !this.isThrown;
    }

    public void Execute()
    {
        if (this.isThrown) return;

        this.Throw();
    }

    protected virtual void Throw()
    {
        Transform objThrown = PlayerWeaponSpawner.Instance.GetObjectSpawnThrow();

        if (objThrown == null) return;

        ItemThrowableCtrl throwableCtrl = objThrown.GetComponent<ItemThrowableCtrl>();

        if (throwableCtrl == null) return;

        //Set Speed
        throwableCtrl.ItemThrowableInputMovement.SetSpeedThrowOverall(this._Speed_Throw);

        //Set des
        Vector2 des = this.GetDestinationByName(PlayerWeaponSpawner.Instance.CurrentThrowItem);

        throwableCtrl.ItemThrowableInputMovement.SetTargetFollow(des);

        PlayerWeaponSpawner.Instance.SpawnObject(objThrown, (Vector2)this._Point_Throw.position);

        this.isThrown = true;

    }

    protected virtual Vector2 GetDestinationByName(ItemType typeItemThrow)
    {
        switch (typeItemThrow)
        {
            case ItemType.Slipper: return (Vector2)DogSpawner.Instance.GetDogNearestPlayer().position;

            case ItemType.Banana:
                Transform playerModel_T = this.PlayerCtrl.PlayerModel.transform;
                return playerModel_T.position + new Vector3(playerModel_T.localScale.x * 1f, 0f);

            case ItemType.Fish:
                break;
        }

        return Vector2.zero;
    }
    // Gọi hàm này khi animation throw kết thúc (qua Animation Event)
    // hoặc dùng Invoke/coroutine nếu không có animation event
    public void OnThrowAnimationJustFinished()
    {
        this.isThrown = false;
    }

}