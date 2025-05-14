using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public int goldAmount { get; private set; } = 1000;     //蹂댁쑀 怨⑤뱶??

    public event Action OnGoldChanged;
    public void GetGold(int gold)
    {
        goldAmount += gold;
        OnGoldChanged?.Invoke();
    }

    public bool UseGold(int gold)
    {   //怨⑤뱶?됱씠 異⑸텇?섎㈃ 泥섎━ ??true ,?꾨땲硫?false 諛섑솚
        if (goldAmount >= gold)
        {
            goldAmount -= gold;
            OnGoldChanged?.Invoke();
            return true;
        }
        else return false;
    }
}
