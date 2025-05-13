using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicTrap : NormalTrap   //?쒖꽦??鍮꾪솢?깊솕瑜?諛섎났?섎뒗 ?⑥젙 
{
    [Header("Trap Settings(Periodic)")]
    [SerializeField] private float activeDuration = 1.5f;     // ?⑥젙???쒖꽦?붾릺???덈뒗 ?쒓컙
    [SerializeField] private float inactiveDuration = 2.0f;   // ?⑥젙??鍮꾪솢?깊솕?섏뼱 ?덈뒗 ?쒓컙

    private bool isActive = false;      //?꾩옱 ?⑥젙 ?쒖꽦???곹깭
    private float activeTimer = 0f;          //二쇨린 蹂寃쎌쓣 ?꾪븳 ??대㉧
    private Collider2D trapCollider;
    private Animator animator;
    private readonly int IsActive = Animator.StringToHash("IsActive");


    private void Start()
    {
        Init();
        SetTrapState(true);   //?쒖꽦???곹깭濡??쒖옉   
    }

    protected override void Update()
    {
        activeTimer -= Time.deltaTime;
        if(activeTimer <= 0f)
        {
            //?⑥젙 ?곹깭 ?꾪솚
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

