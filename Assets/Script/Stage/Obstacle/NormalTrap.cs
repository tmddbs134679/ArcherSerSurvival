using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTrap : MonoBehaviour   //플레이어 접촉 시 피해를 입히는 함정
{
    [Header("Trap Settings(Base)")]
    [SerializeField] protected float damageAmount = 10f;        // 피해량
    [SerializeField] protected LayerMask playerLayer;           // 피해 대상 레이어(플레이어)
    //protected PlayerResource player  데미지 처리를 위한 플레이어 체력 관리 객체

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            //player = other.GetComponent<PlayerResource>();
            TryDealDamage();
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            TryDealDamage();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        /*
         * if(player != null && other.gameObject = player.gameObject){
         *      player = null;
         */
    }

    protected void TryDealDamage()
    {
        //if(player == null) return;
        //데미지 처리
    }
}

