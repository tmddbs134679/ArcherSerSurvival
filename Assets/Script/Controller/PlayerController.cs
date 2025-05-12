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
        base.Awake();
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
        // ?筌뤿굝? ?釉뚯뫊??
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        pRigidbody.velocity = movement * playerStat.Speed;

        isMoving = movement.magnitude > 0.1f;

        animator.SetBool("isMove", movement.magnitude > 0.1f);

        // ??????戮?뱺??????덃뤆???μ쪠??
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

    // ?????띠룆흮???琉우뿰 ?????熬곣뫚???return??濡ル츎 ??貫??
    public Transform GetClosestEnemy()
    {
        if (!isMoving)
        {
            // collider????⑤챷諭??띠룆흮? -> 嶺뚣끉裕???? ?熬곥굥???꾩룄?ｈ굢????ル┰
            Collider2D[] enemiesInRange = new Collider2D[10];

            LayerMask enemyLayer = LayerMask.GetMask("Enemy");
            int count = Physics2D.OverlapBoxNonAlloc(transform.position, targetRange, 0f, enemiesInRange, enemyLayer);

            if(count == 0)
            {
                closestEnemy = null;
                return null;
            }

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


    // ?????띠룆흮???濡?듆 ???롪퍔????怨쀬Ŧ sprite ???????flip
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

    // ??????怨룹꽑?遊붋?????롪퍔???먯?????꾩렮維싧젆??return??濡ル츎 ??貫??
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

    // ??ㅻ?????類ㅼ읉??
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

    // ?????띠룆흮? ?뺢퀡???gizmo
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
