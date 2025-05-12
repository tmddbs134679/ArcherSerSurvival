using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private WeaponController WeaponPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Vector2 targetRange = new Vector2(5, 5);
    private WeaponController weaponController;

    private Rigidbody2D pRigidbody;
    private Animator animator;

    private bool isMoving = false;
    private Transform closestEnemy = null;

    private bool isAttacking = false;
    private float timeLastAttack = float.MaxValue;

    private PlayerStat playerStat;

    void Awake()
    {
        pRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerStat = GetComponent<PlayerStat>();

        if (WeaponPrefab != null)
            weaponController = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponController = GetComponentInChildren<WeaponController>();
    }

    void Update()
    {
        PlayerMove();
        RotateWeaponToTarget();
        AttackDelayHandler();
    }

    void PlayerMove()
    {
        // 인풋 분리
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        pRigidbody.velocity = movement * playerStat.Speed;

        isMoving = movement.magnitude > 0.1f;

        animator.SetBool("isMove", movement.magnitude > 0.1f);

        // 이동시에는 회전값 고정
        if (isMoving)
        {
            Rotate(movement);
            RotateWeapon(-90f);
            weaponController.FlipWeapon(false);
            isAttacking = false;
        }

    }

    void Rotate(Vector2 direction)
    {
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool isLeft = Mathf.Abs(rotation) > 90f;

        spriteRenderer.flipX = isLeft;
    }

    void RotateWeapon(float rotation)
    {
        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotation);
        }
    }

    // 타겟 감지하여 타겟 위치를 return하는 함수
    public Transform GetClosestEnemy()
    {
        if (!isMoving)
        {
            // collider로 적을 감지 -> 최적화를 위해 배열 제한
            Collider2D[] enemiesInRange = new Collider2D[10];

            LayerMask enemyLayer = LayerMask.GetMask("Enemy");
            int count = Physics2D.OverlapBoxNonAlloc(transform.position, targetRange, 0f, enemiesInRange, enemyLayer);

            float minDistance = Mathf.Infinity;

            for (int i = 0; i < count; i++)
            {
                float enemyDistance = Vector2.Distance(transform.position, enemiesInRange[i].transform.position);
                if (enemyDistance < minDistance)
                {
                    minDistance = enemyDistance;
                    closestEnemy = enemiesInRange[i].transform;
                }
            }

            if (closestEnemy != null)
            {
                float targetRangeDistance = targetRange.magnitude * 0.5f;
                float closestEnemyDistance = Vector2.Distance(transform.position, closestEnemy.position);

                if (closestEnemyDistance < targetRangeDistance)
                {
                    return closestEnemy;
                }
                else
                {
                    return null;
                }
            }
        }
        return null;
    }


    // 타겟 감지하면 타겟쪽으로 sprite 회전 및 flip
    void RotateWeaponToTarget()
    {
        if (GetClosestEnemy() != null)
        {
            float rotation = Mathf.Atan2(EnemyDirection().y, EnemyDirection().x) * Mathf.Rad2Deg;

            RotateWeapon(rotation);

            bool isLeft = Mathf.Abs(rotation) > 90f;
            weaponController.FlipWeapon(isLeft);
            spriteRenderer.flipX = isLeft;

            isAttacking = true;
        }
    }

    // 플레이어부터 타겟까지의 방향을 return하는 함수
    Vector2 EnemyDirection()
    {
        Transform target = GetClosestEnemy();
        return (target.position - transform.position).normalized;
    }

    void Attack()
    {
        Transform target = GetClosestEnemy();
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            weaponController.AttackAni();
        }
        
    }

    // 공격 딜레이
    void AttackDelayHandler()
    {
        if (timeLastAttack <= weaponController.Delay)
        {
            timeLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeLastAttack > weaponController.Delay)
        {
            timeLastAttack = 0;
            Attack();
        }
    }

    // 타겟 감지 범위 gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, targetRange);
    }

}
