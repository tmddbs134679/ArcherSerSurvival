using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player 筌ｋ???揶쏅Ŋ?????뮞????쎄쾿?깆???
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
