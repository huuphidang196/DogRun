using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowableInteraction : ObjInteraction
{
    public ItemThrowableCtrl ItemThrowableCtrl => this._ObjCtrl as ItemThrowableCtrl;

    protected bool _HasInteracted = false; // 👉 cờ chặn xử lý nhiều lần

    protected override void OnEnable()
    {
        base.OnEnable();
        // Reset lại mỗi lần được spawn lại từ pool — quan trọng vì object tái sử dụng
        this._HasInteracted = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this._HasInteracted) return; // 👉 đã xử lý rồi thì bỏ qua mọi va chạm tiếp theo
        if (!IsTarget(other.gameObject)) return;

        Interact(other.gameObject);
    }

    public override void Interact(GameObject target)
    {
        if (this.CheckIsDog(target))
        {
            this._HasInteracted = true; // 👉 khóa NGAY khi xác nhận sẽ xử lý, tránh race giữa nhiều trigger cùng frame
            this.ProcessInteractWithDog(target);
            return;
        }
        if (this.CheckIsObstacle(target))
        {
            this._HasInteracted = true;
            this.ProcessInteractWithObstacle(target);
            return;
        }
    }

    protected virtual bool CheckIsObstacle(GameObject target)
    {
        if (target == null) return false;
        return target.layer == LayerMask.NameToLayer("Obstacle");
    }

    protected virtual void ProcessInteractWithObstacle(GameObject target)
    {
        this.ItemThrowableCtrl.ItemThrowableDespawn.Despawn();
    }

    protected virtual void ProcessInteractWithDog(GameObject target)
    {
        DogObjCtrl DogCtrl = target.GetComponent<DogObjCtrl>();
        if (DogCtrl == null) return;

        DogCtrl.DogReceiver.Receive(this); // 👉 giờ chỉ gọi đúng 1 lần duy nhất, tại đúng 1 chỗ
        this.ItemThrowableCtrl.ItemThrowableDespawn.Despawn();
    }

    protected virtual bool CheckIsDog(GameObject target)
    {
        if (target == null) return false;
        DogObjCtrl dog = target.GetComponent<DogObjCtrl>();
        if (dog == null) return false;
        return dog.DogReceiver != null;
    }
}