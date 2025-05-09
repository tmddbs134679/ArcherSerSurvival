using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private WeaponController WeaponPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Vector2 targetRange = new Vector2(5,5);
    private WeaponController weaponController;

    private Rigidbody2D rigidbody;
    private Animator animator;

    private bool isMoving = false;
    private Transform closestEnemy = null;

    private bool isAttacking = false;
    private float timeLastAttack = float.MaxValue;

    private StatHandler statHandler;

    [SerializeField] private float currentHp = 0;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        statHandler = GetComponent<StatHandler>();

        if (WeaponPrefab != null)
            weaponController = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponController = GetComponentInChildren<WeaponController>();
    }

    private void Start()
    {
        currentHp = statHandler.Hp;
    }

    void Update()
    {
        ActionHandler();
        RotateWeaponToTarget();
        AttackDelayHandler();

        // ü�� ���� �׽�Ʈ
        if (Input.GetKeyDown(KeyCode.H))
        {
            ReduceHp(1);
        }
    }

    public void ReduceHp(float reduceHp)
    {
        currentHp -= reduceHp;

        if (currentHp <= 0)
        {
            Death();
        }
    }



    private void Death()
    {
        rigidbody.velocity = Vector3.zero;

        // ������ ����������
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        // ����ϸ� ��� ������Ʈ ����
        foreach (Behaviour componenet in transform.GetComponentsInChildren<Behaviour>())
        {
            componenet.enabled = false;
        }

        // ��� 2�� �� ����
        Destroy(gameObject, 2f);
    }

    void ActionHandler()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rigidbody.velocity = movement * speed;

        isMoving = movement.magnitude > 0.1f;
        
        animator.SetBool("isMove", movement.magnitude > 0.1f);

        // �̵��ÿ��� ȸ���� ����
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

    // Ÿ�� �����Ͽ� Ÿ�� ��ġ�� return�ϴ� �Լ�
    Transform GetClosestEnemy()
    {
        if (!isMoving)
        {
            // collider�� ���� ���� -> ����ȭ�� ���� �迭 ����
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

            if(closestEnemy != null)
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


    // Ÿ�� �����ϸ� Ÿ�������� sprite ȸ�� �� flip
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

    // �÷��̾���� Ÿ�ٱ����� ������ return�ϴ� �Լ�
    Vector2 EnemyDirection()
    {
        Transform target = GetClosestEnemy();
        Debug.Log(target.gameObject.name);
        return (target.position - transform.position).normalized;
    }

    void Attack()
    {
        weaponController.AttackAni();
        weaponController.ShootBullet(EnemyDirection());
    }

    // ���� ������
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

    // Ÿ�� ���� ���� gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, targetRange);
    }

}
