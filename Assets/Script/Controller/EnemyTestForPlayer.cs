using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player 체력 감소 테스트 스크립트
public class EnemyTestForPlayer : MonoBehaviour
{
    [SerializeField] private PlayerStat playerStat;

    LayerMask playerLayer;
    float enemyAtk = 1;

    private void Awake()
    {
        playerLayer = LayerMask.GetMask("Player");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerLayer.value == (1 << collision.gameObject.layer))
        {
            playerStat.Damaged(enemyAtk);
        }
    }
}
