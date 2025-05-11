using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    private ProjectileData data;//비어있는 투사체의 데이터
    private Vector2 Target;//비어있는 공격방향
    private Vector2 angleDirection;//비어있는 선처리 공격방향
    private Rigidbody2D rb;//프리팹한 투사체의 리짓바디
    public string serialName;//이름
    private Coroutine CoroutineTemp;
    public void Init(Vector2 target, Vector2 angleDir, ProjectileData _data)
    {
        Target = target;
        angleDirection = angleDir.normalized;
        data = _data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        StartCoroutine(WrappingInvokeDelay(data.duration));
    }

    private void FixedUpdate()//물리처리
    {
        rb.velocity = angleDirection * data.speed;
        StartCoroutine(AngleDirDelay());
        transform.Rotate(Vector3.forward, data.rotateSpeed * Time.fixedDeltaTime); //프리팹 자체 회전
    }

    void OnTriggerEnter2D(Collider2D collision)//충돌했을 시
    {

        if (collision.GetComponent<EnemyStateMachine>() != null)
        {
            collision.GetComponent<EnemyStateMachine>().Health.DealDamage(data.damage);
            StartCoroutine(WrappingInvokeDelay(0f));//skillShooter의 ReturnToPool() 메서드를 호출
        }

    }
    private IEnumerator WrappingInvokeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ProjectileObjectPool.Instance.Release(serialName, this.gameObject);
    }

    private IEnumerator AngleDirDelay()
    {
        yield return new WaitForSeconds(data.hormingStartDelay);
        Vector2 self = this.transform.position;

        angleDirection = Vector2.Lerp(angleDirection, (Target - self).normalized, Time.deltaTime * data.hormingTurnDelay);
        rb.velocity = angleDirection * data.speed;
    }
}
