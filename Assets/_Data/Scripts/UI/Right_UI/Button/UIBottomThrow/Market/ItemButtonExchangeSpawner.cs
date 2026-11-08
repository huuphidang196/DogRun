using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButtonExchangeSpawner : Spawner<ItemButtonExchangeSpawner>
{
    protected override void LoadHolder()
    {
        if (this._Holder != null) return;

        this._Holder = transform.Find("Viewport").Find("Content");
    }
}
