using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private WeaponController WeaponPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float attackRange = 15f;
    private WeaponController weaponController;

    private Rigidbody2D rigidbody;
    private Animator animator;
    
    private Transform closestEnemy = null;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        if (WeaponPrefab != null)
            weaponController = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponController = GetComponentInChildren<WeaponController>();
    }



    void Update()
    {
        ActionHandler();
        if (GetClosestEnemy() != null)
        {
            Debug.Log(GetClosestEnemy().name);
        }
        

    }

    void ActionHandler()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rigidbody.velocity = movement * speed;

        animator.SetBool("isMove", movement.magnitude > 0.1f);

        if (movement.magnitude > 0.1f)
        {
            Rotate(movement);
        }

    }

    void Rotate(Vector2 direction)
    {
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool isLeft = Mathf.Abs(rotation) > 90f;

        spriteRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotation);
        }
        weaponController.RotateWeapon(isLeft);
    }

    Transform GetClosestEnemy()
    {
        Vector2 range = new Vector2(3, 3);
        float angle = 0f;
        LayerMask enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(transform.position, range, angle, enemyLayer);
        
        float minDistance = Mathf.Infinity;

        foreach (Collider2D collider in enemiesInRange)
        {
            float enemyDistance = Vector2.Distance(transform.position, collider.transform.position);
            if (enemyDistance < minDistance)
            {
                minDistance = enemyDistance;
                closestEnemy = collider.transform;
            }
        }
        return closestEnemy;
    }

}
