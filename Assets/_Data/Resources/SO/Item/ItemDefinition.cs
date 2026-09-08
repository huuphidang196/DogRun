using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Item", menuName = "SO/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    public ItemType ItemType;
    public bool Throwable;
    public Sprite Icon;
}
