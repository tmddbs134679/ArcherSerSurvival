using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public int goldAmount { get; private set; } = 1000;     //보유 골드량

    public event Action OnGoldChanged;
    public void GetGold(int gold)
    {
        goldAmount += gold;
        OnGoldChanged?.Invoke();
    }

    public bool UseGold(int gold)
    {   //골드량이 충분하면 처리 후 true ,아니면 false 반환
        if (goldAmount >= gold)
        {
            goldAmount -= gold;
            OnGoldChanged?.Invoke();
            return true;
        }
        else return false;
    }
}
