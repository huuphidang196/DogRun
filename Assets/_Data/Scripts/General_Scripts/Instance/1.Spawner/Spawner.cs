using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : Singleton<T>, IDespawnable where T : StandMonoBehaviour
{
    [SerializeField] protected List<Transform> _Prefabs;

    [SerializeField] protected Transform _Holder;
    public Transform Holder => this._Holder;

    [SerializeField] protected List<Transform> _Pool_Object;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadPrefabs();
        this.LoadHolder();
    }

    protected virtual void LoadHolder()
    {
        if (this._Holder != null) return;

        this._Holder = transform.Find("Holder");
    }

    protected virtual void LoadPrefabs()
    {
        if (this._Prefabs.Count > 0) return;

        Transform Prefabs_Obj = transform.Find("Prefabs");

        if (Prefabs_Obj == null) return;

        foreach (Transform item in Prefabs_Obj)
        {
            this._Prefabs.Add(item);
            item.gameObject.SetActive(false);
        }
    }
    public virtual Transform GetPrefabsByName(string name)
    {
        if (this._Prefabs.Count == 0) return null;
        Transform prefab = this._Prefabs.Find(x => x.gameObject.name == name);
        if (prefab == null) return null; // nên thêm check này để tránh NullReferenceException nếu không tìm thấy prefab

        Transform obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = name; // 👉 đặt lại tên đúng, loại bỏ hậu tố "(Clone)", đảm bảo khớp với Find() sau này
        return obj;
    }

    public virtual void DespawnObject(Transform obj)
    {
        if (obj == null) return;
        if (this._Pool_Object.Contains(obj)) return; // tránh add trùng

        obj.gameObject.SetActive(false);
        this._Pool_Object.Add(obj);
    }

    public virtual Transform GetObjectSpawn(string name)
    {
        Transform obj = this._Pool_Object.Find(x => x.name.Contains(name));
        if (obj != null)
        {
            this._Pool_Object.Remove(obj);
            return obj;
        }

        obj = this.GetPrefabsByName(name);

        return obj;
    }

    public virtual Transform SpawnObject(string name, Vector2 pos)
    {
        Transform obj = this.GetObjectSpawn(name);

        this.SpawnObject(obj, pos);

        return obj;
    }
    public virtual void SpawnObject(Transform obj, Vector2 pos)
    {
        if (obj == null) return;

        obj.parent = this._Holder;

        obj.gameObject.name += "_" + this._Holder.childCount;

        obj.position = pos;
        obj.gameObject.SetActive(true);

        // 👉 tự động gán spawner cho object nếu nó có ObjDespawnAbstract
        ObjCtrl objCtrl = obj.GetComponent<ObjCtrl>();
        if (objCtrl == null) return;

        ObjDespawnAbstract despawnCtrl = objCtrl.ObjDespawnAbstract;

        if (despawnCtrl != null)
        {
            despawnCtrl.SetSpawner(this);
        }

    }
}
