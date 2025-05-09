using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Vector2 direction;
    private Rigidbody2D rigidbody;
    private Transform pivot;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if(transform.childCount > 0)
        {
            pivot = transform.GetChild(0);
        }
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
}
