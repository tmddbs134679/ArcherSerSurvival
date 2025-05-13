using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class Goblin : MonoBehaviour
{
    [SerializeField] private int maxBounce = 5;
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 currentDir;
    private int bounceCount;
    private Action onComplete;
    private bool isAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Attack(Action onComplete)
    {
        this.onComplete = onComplete;
        bounceCount = 0;
        isAttacking = true;

        currentDir = (this.GetComponent<GoblinStateMachine>().Player.transform.position - transform.position).normalized;
        rb.velocity = currentDir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAttacking) return;

        ContactPoint2D contact = collision.contacts[0];
        Vector2 normal = contact.normal;

        if (collision.collider == GetComponent<Collider2D>())
            return;

        currentDir = Vector2.Reflect(currentDir, normal);
        rb.velocity = currentDir * speed;

        bounceCount++;
        if (bounceCount >= maxBounce)
        {
            EndAttack();
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
        rb.velocity = Vector2.zero;
        onComplete?.Invoke();
    }
}
