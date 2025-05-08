using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicTrap : NormalTrap   //활성화/비활성화를 반복하는 함정 
{
    [Header("Trap Settings(Periodic)")]
    [SerializeField] private float activeDuration = 1.5f;     // 함정이 활성화되어 있는 시간
    [SerializeField] private float inactiveDuration = 2.0f;   // 함정이 비활성화되어 있는 시간

    private bool isActive = false;      //현재 함정 활성화 상태
    private float timer = 0f;          //주기 변경을 위한 타이머
    private Collider2D trapCollider;
    private Animator animator;
    private readonly int IsActive = Animator.StringToHash("IsActive");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        trapCollider = GetComponent<Collider2D>();
    }
    private void Start()
    {     
        SetTrapState(true);   //비활성화 상태로 시작   
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            //함정 상태 전환
            SetTrapState(!isActive);
        }
    }

    void SetTrapState(bool newActiveState)
    {
        isActive = newActiveState;
        animator.SetBool(IsActive, isActive);
        if (isActive)
        {
            timer = activeDuration;
            CheckAndDamagePlayerInside();
            
        }
        else
        {
            timer = inactiveDuration;
        }   
    }

    void CheckAndDamagePlayerInside()
    {   //함정 활성화 시, 플레이어가 이미 밟고 있으면 데미지 처리
        //OverlapBox 를 활용하여 플레이어 레이어 감지
        Bounds trapBounds = trapCollider.bounds;

        Collider2D playerColliderHit = Physics2D.OverlapBox(
            trapBounds.center,  trapBounds.size, 0, playerLayer);

        if (playerColliderHit != null)
        {
            //데미지 처리
        }
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && ((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            //데미지 처리
        }
    }
}

