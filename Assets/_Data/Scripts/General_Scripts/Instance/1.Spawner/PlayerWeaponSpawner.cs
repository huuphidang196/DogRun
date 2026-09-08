using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSpawner : Spawner<PlayerWeaponSpawner>
{
    [SerializeField] protected ItemType _CurrentThrowItem;
    public ItemType CurrentThrowItem => this._CurrentThrowItem;

    public virtual Transform GetObjectSpawnThrow()
    {
        if (this._CurrentThrowItem == ItemType.None) return null;

        return this.GetObjectSpawn(this._CurrentThrowItem.ToString());
    }

    public void SelectThrowItem(ItemType type)
    {
        _CurrentThrowItem = type;
    }

}
public interface IDespawnable
{
    void DespawnObject(Transform obj);
}