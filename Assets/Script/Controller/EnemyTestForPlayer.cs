using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player 泥대젰 媛먯냼 ?뚯뒪???ㅽ겕由쏀듃
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
