using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicTrap : NormalTrap   //Ȱ��ȭ/��Ȱ��ȭ�� �ݺ��ϴ� ���� 
{
    [Header("Trap Settings(Periodic)")]
    [SerializeField] private float activeDuration = 1.5f;     // ������ Ȱ��ȭ�Ǿ� �ִ� �ð�
    [SerializeField] private float inactiveDuration = 2.0f;   // ������ ��Ȱ��ȭ�Ǿ� �ִ� �ð�

    private bool isActive = false;      //���� ���� Ȱ��ȭ ����
    private float activeTimer = 0f;          //�ֱ� ������ ���� Ÿ�̸�
    private Collider2D trapCollider;
    private Animator animator;
    private readonly int IsActive = Animator.StringToHash("IsActive");

    private void Start()
    {
        Init();
        SetTrapState(true);   //Ȱ��ȭ ���·� ����   
    }

    private void Update()
    {
        activeTimer -= Time.deltaTime;
        if(activeTimer <= 0f)
        {
            //���� ���� ��ȯ
            SetTrapState(!isActive);
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
        if (isActive &&  ((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            TryDealDamage();
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        if (isActive && ((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            TryDealDamage();
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}

