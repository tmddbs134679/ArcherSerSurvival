using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    private ProjectileData data;//비어있는 폭발체의 데이터
    private Vector2 Target;//비어있는 공격방향
    private Rigidbody2D rb;//프리팹한 투사체의 리짓바디
    public string serialName;//이름

        public float radius = 0f;           // 현재 반지름
    public float maxRadius = 2.5f;        // 최대 반지름
    public float growSpeed = 0.5f;        // 초당 커지는 속도

    public void Init(Vector2 target, Vector2 angleDir, ProjectileData _data)
    {
        Target = target;
        data = _data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        StartCoroutine(WrappingInvokeDelay(data.duration));
    }

    private void Update()//물리처리
    {
        if (radius < maxRadius)
        {
                        radius += growSpeed * Time.deltaTime;
            transform.localScale = new Vector2(radius,radius);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)//충돌했을 시
    {

        if (collision.GetComponent<EnemyStateMachine>() != null) //&&Target.tag==collsion.tag or layermask 비교
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

}
