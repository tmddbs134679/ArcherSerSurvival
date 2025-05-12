using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    private ProjectileData data;//???닷젆???덈츎 ???뚯궪??怨몃꺄 ??⑥щ턄??
    private Vector2 Target;//???닷젆???덈츎 ??ㅻ??곗띁爾?釉띿깿
    private Rigidbody2D rb;//?熬곣뱿遊?獄?낮由???亦낆?럸?怨몃꺄 ?洹먮봿痍귞뛾?낅???
    public string serialName;//???藥?

        public float radius = 0f;           // ?熬곣뫗???꾩룇瑗???
    public float maxRadius = 2.5f;        // 嶺뚣끉裕? ?꾩룇瑗???
    public float growSpeed = 0.5f;        // ?貫?????ｋ걠??????쒖┣

    public void Init(Vector2 target, Vector2 angleDir, ProjectileData _data)
    {
        Target = target;
        data = _data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        StartCoroutine(WrappingInvokeDelay(data.duration));
    }

    private void Update()//??좊닑?怨삠럸?濡〓뉴
    {
        if (radius < maxRadius)
        {
                        radius += growSpeed * Time.deltaTime;
            transform.localScale = new Vector2(radius,radius);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)//?寃몃쳳????깅굵 ??
    {

        if (collision.GetComponent<EnemyStateMachine>() != null) //&&Target.tag==collsion.tag or layermask ?????
        {
            collision.GetComponent<BaseStat>().Damaged(data.damage);
            StartCoroutine(WrappingInvokeDelay(0f));//skillShooter??ReturnToPool() 嶺뚮∥?꾥땻??? ?筌뤾쑵??
        }

    }
    private IEnumerator WrappingInvokeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ProjectileObjectPool.Instance.Release(serialName, this.gameObject);
    }

}
