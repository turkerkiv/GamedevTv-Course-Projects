using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;

    Bank _bank;

    private void Awake()
    {
        _bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (_bank == null) { return; }
        _bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if (_bank == null) { return; }
        _bank.WithDraw(goldPenalty);
    }
}
