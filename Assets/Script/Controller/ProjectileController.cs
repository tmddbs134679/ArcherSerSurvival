using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Vector2 direction;
    private Rigidbody2D rigidbody;
    private Transform pivot;

    LayerMask enemyLayer;
    LayerMask levelLayer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if(transform.childCount > 0)
        {
            pivot = transform.GetChild(0);
        }
        enemyLayer = LayerMask.GetMask("Enemy");
        levelLayer = LayerMask.GetMask("Level");
    }

    private void Update()
    {
        rigidbody.velocity = direction * speed;
    }

    public void Init(Vector2 direction)
    {
        this.direction = direction;
        transform.right = this.direction;

        if (direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelLayer.value == (1<<collision.gameObject.layer))
        {
            DestroyProjectile();
        }
        else if (enemyLayer.value == (1<<collision.gameObject.layer))
        {
            // enemy와 충돌하면 enemy 피격, 일단 파괴
            Destroy(collision.gameObject);
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
