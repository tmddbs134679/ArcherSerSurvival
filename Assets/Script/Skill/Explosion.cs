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

    public float radius;           //시작 반경
    public float maxRadius;      //끝 반경
    public float growSpeed;      //반경 속도
public GameObject WarningZone; //마법진 스프라이트 이미지

private bool hasDamaged = false;
    void Awake()
    {
        WarningZone.SetActive(false);
    }
    public void Init(GameObject launcher, GameObject target, ChangedSkillData data)
    {
                WarningZone.SetActive(true);
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
            radius += (growSpeed * Time.deltaTime)*1.05f;
            transform.localScale = new Vector2(radius, radius);
        }
        else{
                    StartCoroutine(WrappingInvokeDelay(0));
        }
    }

void OnTriggerStay2D(Collider2D collision)
{
    if (Target.layer == collision.gameObject.layer && !hasDamaged)
    {
        if (radius >= maxRadius-0.05f)
        {
            collision.GetComponent<BaseStat>().Damaged(Data.damage);
            hasDamaged = true;
        }
    }
}
    private IEnumerator WrappingInvokeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        radius=0;
                WarningZone.SetActive(false);
        ProjectileObjectPool.Instance.Release(serialName, this.gameObject);
    }

}
