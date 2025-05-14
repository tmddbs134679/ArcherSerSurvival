using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop :UsableITem
{
    [SerializeField] private int minGoldAmount = 1;
    [SerializeField] private int maxGoldAmount = 5;
    SFXControl playSFX;

    private void Start()
    {
        playSFX = GetComponent<SFXControl>();
    }
    protected override void Use(GameObject target)
    {
        int gold = Random.Range(minGoldAmount, maxGoldAmount);
        PlayerResource player = target.GetComponent<PlayerResource>();

        if (player != null)
        {
            player.GetGold(gold);
        }

        if(playSFX != null)
        {
            playSFX.PlaySoundEffect();
        }

        base.Use(target);
    }
}
