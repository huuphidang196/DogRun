using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDespawnByTime : ObjDespawnAbstract
{
    [Header("Despawn Time - thời gian nếu không có sự kiện gì thì tắt hẳn")]
    [SerializeField] protected float _Time_LifeTime = 3f;

    protected Coroutine _DespawnCoroutine;
    //protected override void ResetValue()
    //{
    //    base.ResetValue();

    //    this._SpawnerObjectName = this._ObjCtrl.gameObject.name;
    //}

    protected override void OnActivateDespawn()
    {
        this.StartDespawnTimer(); // dùng _LifeTime kế thừa từ ObjDespawnAbstract, đếm giai đoạn "sống"
    }
    protected virtual void StartDespawnTimer()
    {
        this._DespawnDelayCoroutine = StartCoroutine(DespawnAfterTimer());
    }
    protected override void OnDeactivateDespawn()
    {
        base.OnDeactivateDespawn(); // dừng coroutine đếm _LifeTime (nếu còn chạy)

        // Dừng luôn coroutine giai đoạn despawn delay, tránh trùng lặp khi object bị tắt đột ngột giữa chừng
        if (this._DespawnDelayCoroutine != null)
        {
            this.StopCoroutine(this._DespawnDelayCoroutine);
            this._DespawnDelayCoroutine = null;
        }
    }
    protected virtual IEnumerator DespawnAfterTimer()
    {
        if (this._Spawner == null)
        {
            Debug.LogWarning($"{this.name} chưa được SetSpawner, không thể despawn đúng chỗ.");
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSeconds(this._Time_LifeTime);

     //   this._ObjCtrl.gameObject.name = this._SpawnerObjectName;

        this._Spawner.DespawnObject(this._ObjCtrl.transform);
    }
}
