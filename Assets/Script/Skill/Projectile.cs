using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    private ChangedSkillData data;//비어?�는 ?�사체의 ?�이??
    private Vector2 Target;//비어?�는 공격방향
    private Vector2 angleDirection;//비어?�는 추적 공격방향
    private Rigidbody2D rb;//?�리?�한 ?�사체의 리짓바디
    public string serialName;//?�름
    public void Init(Vector2 target, Vector2 angleDir, ChangedSkillData _data)
    {
        Target = target;
        angleDirection = angleDir.normalized;
        data = _data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        StartCoroutine(WrappingInvokeDelay(data.duration));
    }

    private void FixedUpdate()//����ó��
    {
        rb.velocity = angleDirection * data.speed;
        StartCoroutine(AngleDirDelay());
        transform.Rotate(Vector3.forward, data.rotateSpeed * Time.fixedDeltaTime); //������ ��ü ȸ��
    }

    void OnTriggerEnter2D(Collider2D collision)//�浹���� ��
    {
        if (collision.GetComponent<EnemyStateMachine>() != null) //&&Target.tag==collsion.tag or layermask ��
        {
            collision.GetComponent<BaseStat>().Damaged(data.damage);
            StartCoroutine(WrappingInvokeDelay(0f));//skillShooter??ReturnToPool() 메서?��? ?�출
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
