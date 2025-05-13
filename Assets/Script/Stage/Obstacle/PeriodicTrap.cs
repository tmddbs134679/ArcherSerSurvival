using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicTrap : NormalTrap   //활성화/비활성화를 반복하는 함정 
{
    [Header("Trap Settings(Periodic)")]
    [SerializeField] private float activeDuration = 1.5f;     // 함정이 활성화되어 있는 시간
    [SerializeField] private float inactiveDuration = 2.0f;   // 함정이 비활성화되어 있는 시간

    private bool isActive = false;      //현재 함정 활성화 상태
    private float activeTimer = 0f;          //주기 변경을 위한 타이머
    private Collider2D trapCollider;
    private Animator animator;
    private readonly int IsActive = Animator.StringToHash("IsActive");


    private void Start()
    {
        Init();
        SetTrapState(true);   //활성화 상태로 시작   
    }

    protected override void Update()
    {
        activeTimer -= Time.deltaTime;
        if(activeTimer <= 0f)
        {
            //함정 상태 전환
            SetTrapState(!isActive);
        }

        if (isActive)
        {
            base.Update();
        }
    }

    void Init()
    {
        animator = GetComponentInChildren<Animator>();
        trapCollider = GetComponent<Collider2D>();
    }

    void SetTrapState(bool newActiveState)
    {
        isActive = newActiveState;
        trapCollider.enabled = isActive;
        animator.SetBool(IsActive, isActive);
        if (isActive)
        {
            activeTimer = activeDuration;   
        }
        else
        {
            activeTimer = inactiveDuration;
        }   
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            player = other.GetComponent<PlayerStat>();
            if (isActive)
            {
                TryDealDamage();
            }
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}

