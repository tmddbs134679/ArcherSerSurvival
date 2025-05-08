using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTrap : MonoBehaviour   //플레이어 접촉 시 피해를 입히는 함정
{
    [Header("Trap Settings(Base)")]
    [SerializeField] protected float damageAmount = 10f;        // 피해량
    [SerializeField] protected LayerMask playerLayer;           // 피해 대상 레이어(플레이어)

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            //데미지 처리
        }
    }

}

