using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NormalTrap : MonoBehaviour   //?뚮젅?댁뼱 ?묒큺 ???쇳빐瑜??낇엳???⑥젙
{
    [Header("Trap Settings(Base)")]
    [SerializeField] protected float damageAmount = 10f;        // ?쇳빐??
    [SerializeField] protected LayerMask playerLayer;           // ?쇳빐 ????덉씠???뚮젅?댁뼱)
    protected PlayerStat player;  //?곕?吏 泥섎━瑜??꾪븳 ?뚮젅?댁뼱 泥대젰 愿由?媛앹껜
    protected float damageDelay = .5f;
    private float timer = 0f;

    protected virtual void Update()
    {
        if (player != null && damageDelay <= timer)
        {
            TryDealDamage();
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            player = other.GetComponent<PlayerStat>();
            TryDealDamage();
        }
    }


    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (player != null && other.gameObject == player.gameObject)
        {
            player = null;
        }
    }

    protected void TryDealDamage()
    {
        if(player == null) return;
        player.Damaged(damageAmount);
    }
}

