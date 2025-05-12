using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop :UsableITem
{
    [SerializeField] private int minGoldAmount = 1;
    [SerializeField] private int maxGoldAmount = 5;
    protected override void Use(GameObject target)
    {
        int gold = Random.Range(minGoldAmount, maxGoldAmount);
        //골드 추가
        base.Use(target);
    }
}
