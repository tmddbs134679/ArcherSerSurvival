using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTreasureChest : MonoBehaviour
{
    private readonly int IsOpen = Animator.StringToHash("IsOpen");
    [SerializeField] protected LayerMask playerLayer;           // 플레이어 레이어
    Animator animator;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // GameManager를 통해 게임 클리어 상태인지 먼저 확인 (if 문에 추가)
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            animator.SetTrigger(IsOpen);
            //스테이지 클리어, 보상 처리
        }
    }
}
