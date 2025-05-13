using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    private ChangedSkillData Data;//비어있는 투사체의 데이터
    private GameObject Target;//공격대상
    public GameObject Launcher;//발사체
    private Rigidbody2D rb;//프리팹한 투사체의 리짓바디
    public string serialName;//이름

    public float radius = 0f;           //시작 반경
    public float maxRadius = 2.5f;        //끝 반경
    public float growSpeed = 0.5f;        //반경 속도

    public void Init(GameObject launcher, GameObject target, ChangedSkillData data)
    {
        Launcher = launcher;
        Target = target;
        Data = data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
    }

    private void Update()//물리처리
    {
        if (radius < maxRadius)
        {
            radius += growSpeed * Time.deltaTime;
            transform.localScale = new Vector2(radius, radius);
        }
        else{
                    StartCoroutine(WrappingInvokeDelay(Data.duration));
        }
    }
    void OnTriggerEnter2D(Collider2D collision)//충돌했을 시
    {
        Debug.Log("장판과 충돌했습니다!");
        if (Target.layer == collision.gameObject.layer)
        {
            collision.GetComponent<BaseStat>().Damaged(Data.damage);
            StartCoroutine(WrappingInvokeDelay(0f));
        }
    }
    private IEnumerator WrappingInvokeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        radius=0;
        ProjectileObjectPool.Instance.Release(serialName, this.gameObject);
    }

}
