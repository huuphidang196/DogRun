using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DogSpawner : Spawner<DogSpawner>
{
    public int Max_Dog_Spawned = 5;
    protected override void Start()
    {
        base.Start();

        InvokeRepeating(nameof(SpawnDog), 0, 10f);
    }

    private void SpawnDog()
    {
        if (this._Holder.childCount >= this.Max_Dog_Spawned) return;
        string nameDog = "Dog_01";


        Transform dog = SpawnObject(nameDog, InputManagerInstance.Instance.GetRandomPointInPatrolArea());
    //    Debug.Log(InputManagerInstance.Instance.GetRandomPointInPatrolArea());
        DogObjCtrl dogObjCtrl = dog.GetComponent<DogObjCtrl>();

        if (dogObjCtrl == null) return;

        dogObjCtrl.DogModel.SpriteRenderer.sortingOrder = dog.GetSiblingIndex() + 1;
    }

    public virtual Transform GetDogNearestPlayer()
    {
        if (this._Holder == null || this._Holder.childCount == 0) return null;
        if (PlayerCtrl.Instance == null) return null;

        Vector2 playerPos = PlayerCtrl.Instance.transform.position;
        Transform nearestDog = null;
        float minSqrDistance = float.MaxValue;

        foreach (Transform dog in this._Holder)
        {
            if (!dog.gameObject.activeSelf) continue; // bỏ qua dog đang despawn

            float sqrDistance = ((Vector2)dog.position - playerPos).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                nearestDog = dog;
            }
        }

        return nearestDog;
    }
}