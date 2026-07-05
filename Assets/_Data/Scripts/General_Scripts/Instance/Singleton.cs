using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : StandMonoBehaviour where T : StandMonoBehaviour
{
    public static T Instance;

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
