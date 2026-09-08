using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjDespawnAbstract : ObjCtrlAbstract
{
    [Header("Despawn Delay - thời gian chạy animation/VFX trước khi tắt hẳn")]
    [SerializeField] protected float _Time_DespawnDelay= 1f; // tự despawn sau X giây nếu không va chạm gì
   // [SerializeField] protected string _SpawnerObjectName; // tên object để despawn đúng chỗ trong pool

    protected Coroutine _DespawnDelayCoroutine;
    protected IDespawnable _Spawner; // 👉 không biết cụ thể là Spawner nào, chỉ cần biết nó despawn được

    public virtual void SetSpawner(IDespawnable spawner)
    {
        this._Spawner = spawner;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.OnActivateDespawn();
    }
    // 👉 Class con override để bắt đầu theo dõi điều kiện despawn riêng (VD: bắt đầu đếm giờ)
    protected virtual void OnActivateDespawn() { }
    protected override void OnDisable()
    {
        base.OnDisable();

        this.OnDeactivateDespawn();
    }
    
    protected virtual void OnDeactivateDespawn()
    {
        // Dừng coroutine cũ khi bị disable, tránh trùng lặp/leak
        if (this._DespawnDelayCoroutine != null)
        {
            this.StopCoroutine(this._DespawnDelayCoroutine);
            this._DespawnDelayCoroutine = null;
        }
    }

    protected virtual IEnumerator DespawnAfterDelay()
    {
        if (this._Spawner == null)
        {
            Debug.LogWarning($"{this.name} chưa được SetSpawner, không thể despawn đúng chỗ.");
            yield break;
        }

        this.OnDespawnTriggered(); // 👉 đúng ý nghĩa: "sự kiện bắt đầu chuỗi despawn", chạy animation/VFX ở đây
        yield return new WaitForSeconds(this._Time_DespawnDelay);
        this._Spawner.DespawnObject(this._ObjCtrl.transform);
    }

    // 👉 Class con override để chạy animation/VFX ngay khi TRIGGER despawn, trước khi object thực sự bị tắt
    protected virtual void OnDespawnTriggered() { }

    public virtual void Despawn()
    {
        this._DespawnDelayCoroutine = this.StartCoroutine(this.DespawnAfterDelay());
    }

}
