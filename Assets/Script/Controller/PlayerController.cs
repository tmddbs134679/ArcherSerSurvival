using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : Singleton<PlayerController>
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

    [SerializeField] private float currentHp = 0;

    public List<GameObject> skillList = new List<GameObject>();

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
        // ?�풋 분리
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        pRigidbody.velocity = movement * playerStat.Speed;

        isMoving = movement.magnitude > 0.1f;

        animator.SetBool("isMove", movement.magnitude > 0.1f);

        // ?�동?�에???�전�?고정
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

    // ?��?감�??�여 ?��??�치�?return?�는 ?�수
    public Transform GetClosestEnemy()
    {
        if (!isMoving)
        {
            // collider�??�을 감�? -> 최적?��? ?�해 배열 ?�한
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


    // ?��?감�??�면 ?�겟쪽?�로 sprite ?�전 �?flip
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

    // ?�레?�어부???�겟까지??방향??return?�는 ?�수
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

    // 공격 ?�레??
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

    // ?��?감�? 범위 gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, targetRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Portal"))
        {
            GameManager.Instance.NextRoom();
        }
    }

}
