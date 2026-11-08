using System;
using UnityEngine;

public class PlayerWallet : PlayerCtrlAbstract
{
    [SerializeField] private int _Money;

    public int Money => _Money;

    public event Action<int> OnMoneyChanged;

    public virtual bool HasEnoughMoney(int amount)
    {
        return amount >= 0 && _Money >= amount;
    }

    public virtual bool SpendMoney(int amount)
    {
        if (amount <= 0 || !HasEnoughMoney(amount))
            return false;

        _Money -= amount;

        OnMoneyChanged?.Invoke(_Money);

        return true;
    }

    public virtual void AddMoney(int amount)
    {
        if (amount <= 0) return;

        _Money += amount;

        OnMoneyChanged?.Invoke(_Money);
    }
}