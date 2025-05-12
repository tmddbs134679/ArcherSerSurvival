using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    private ProjectileData data;//??쑴堉??덈뮉 ??而삼㎗?곸벥 ?怨쀬뵠??
    private Vector2 Target;//??쑴堉??덈뮉 ?⑤벀爰썼쳸?븍샨
    private Rigidbody2D rb;//?袁ⓥ봺?諭곷립 ??沅쀯㎗?곸벥 ?귐딆췂獄쏅뗀逾?
    public string serialName;//??已?

        public float radius = 0f;           // ?袁⑹삺 獄쏆꼷???
    public float maxRadius = 2.5f;        // 筌ㅼ뮆? 獄쏆꼷???
    public float growSpeed = 0.5f;        // ?λ뜄???뚣끉?????얜즲

    public void Init(Vector2 target, Vector2 angleDir, ProjectileData _data)
    {
        Target = target;
        data = _data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        StartCoroutine(WrappingInvokeDelay(data.duration));
    }

    private void Update()//?얠눖?곻㎗?롡봺
    {
        if (radius < maxRadius)
        {
                        radius += growSpeed * Time.deltaTime;
            transform.localScale = new Vector2(radius,radius);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)//?겸뫖猷??됱뱽 ??
    {

        if (collision.GetComponent<EnemyStateMachine>() != null) //&&Target.tag==collsion.tag or layermask ??쑨??
        {
            collision.GetComponent<BaseStat>().Damaged(data.damage);
            StartCoroutine(WrappingInvokeDelay(0f));//skillShooter??ReturnToPool() 筌롫뗄苑??? ?紐꾪뀱
        }

    }
    private IEnumerator WrappingInvokeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ProjectileObjectPool.Instance.Release(serialName, this.gameObject);
    }

}
