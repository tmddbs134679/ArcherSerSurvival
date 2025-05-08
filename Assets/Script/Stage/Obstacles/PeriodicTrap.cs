using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicTrap : NormalTrap   //Ȱ��ȭ/��Ȱ��ȭ�� �ݺ��ϴ� ���� 
{
    [Header("Trap Settings(Periodic)")]
    [SerializeField] private float activeDuration = 1.5f;     // ������ Ȱ��ȭ�Ǿ� �ִ� �ð�
    [SerializeField] private float inactiveDuration = 2.0f;   // ������ ��Ȱ��ȭ�Ǿ� �ִ� �ð�

    private bool isActive = false;      //���� ���� Ȱ��ȭ ����
    private float timer = 0f;          //�ֱ� ������ ���� Ÿ�̸�
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
        SetTrapState(true);   //��Ȱ��ȭ ���·� ����   
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            //���� ���� ��ȯ
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
    {   //���� Ȱ��ȭ ��, �÷��̾ �̹� ��� ������ ������ ó��
        //OverlapBox �� Ȱ���Ͽ� �÷��̾� ���̾� ����
        Bounds trapBounds = trapCollider.bounds;

        Collider2D playerColliderHit = Physics2D.OverlapBox(
            trapBounds.center,  trapBounds.size, 0, playerLayer);

        if (playerColliderHit != null)
        {
            //������ ó��
        }
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && ((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            //������ ó��
        }
    }
}

