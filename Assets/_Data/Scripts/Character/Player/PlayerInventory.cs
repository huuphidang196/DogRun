using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : PlayerCtrlAbstract
{
    [SerializeField] protected List<ItemData> items;
    public event System.Action<ItemType> OnItemAmountChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (this.PlayerCtrl.PlayerModel != null)
            this.PlayerCtrl.PlayerModel.OnThrowAnimationEnd += this.OnThrowAnimationJustFinished;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (this.PlayerCtrl.PlayerModel != null)
            this.PlayerCtrl.PlayerModel.OnThrowAnimationEnd -= this.OnThrowAnimationJustFinished;
    }

    protected virtual void OnThrowAnimationJustFinished()
    {
        this.RemoveItem(PlayerWeaponSpawner.Instance.CurrentThrowItem);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadAllItem();
    }
    protected virtual void LoadAllItem()
    {
        if (this.items.Count > 0) return;

        items.Clear();

        ItemDefinition[] definitions =
            Resources.LoadAll<ItemDefinition>("SO/Item");

        foreach (ItemDefinition definition in definitions)
        {
            items.Add(new ItemData(definition));
        }
    }

    //------------------------------------
    // Add
    //------------------------------------

    public void AddItem(ItemType type, int amount = 1)
    {
        ItemData item = items.Find(x =>
            x.ItemDefinitionSO.ItemType == type);

        if (item == null)
            return;

        item.Amount += amount;

        OnItemAmountChanged?.Invoke(type);
    }

    //------------------------------------
    // Remove
    //------------------------------------

    public bool RemoveItem(ItemType type, int amount = 1)
    {
        ItemData item = items.Find(x =>
            x.ItemDefinitionSO.ItemType == type);

        if (item == null)
            return false;

        if (item.Amount < amount)
            return false;

        item.Amount -= amount;

        OnItemAmountChanged?.Invoke(type);

        return true;
    }

    //------------------------------------
    // Count
    //------------------------------------

    public int GetAmount(ItemType itemType)
    {
        ItemData item =
            items.Find(x => x.ItemDefinitionSO.ItemType == itemType);

        if (item == null)
            return 0;

        return item.Amount;
    }

    //------------------------------------
    // Has
    //------------------------------------

    public bool HasItem(ItemType itemType)
    {
        return GetAmount(itemType) > 0;
    }

    public virtual bool HasItemThrowable()
    {
        return items.Exists(item => item.ItemDefinitionSO.Throwable && item.Amount > 0);
    }

    public virtual bool HasPlantableItem()
    {
        foreach (ItemData item in items)
        {
            if (item == null || item.Amount <= 0)
                continue;

            if (item.ItemDefinitionSO.Throwable)
                continue;

            switch (item.ItemDefinitionSO.ItemType)
            {
                case ItemType.RiceSeeds:
                case ItemType.CabbageSeeds:
                case ItemType.PumpkinSeeds:
                    return true;
            }
        }

        return false;
    }
}

[System.Serializable]
public class ItemData
{
    public ItemDefinition ItemDefinitionSO;

    public int Amount;

    public ItemData(ItemDefinition definition)
    {
        ItemDefinitionSO = definition;
        Amount = 0;
    }
    public ItemData(ItemDefinition definition, int count)
    {
        ItemDefinitionSO = definition;
        Amount = count;
    }
}

[System.Serializable]
public enum ItemType
{
    None,
    Slipper,
    Banana,
    Fish,

    RiceSeeds,
    CabbageSeeds,
    PumpkinSeeds,

    HarvestedRice,
    HarvestedPumpkin,
    HarvestedCabbage
}