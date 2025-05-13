using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ChangedSkillData Data;//비어있는 투사체의 데이터
    private GameObject Target;//공격대상
    public GameObject Launcher;//발사체
    private Vector2 angleDirection;//비어있는 추적 공격방향
    private Rigidbody2D rb;//프리팹한 투사체의 리짓바디
    public string serialName;//이름

    public void Init(GameObject launcher, GameObject target, Vector2 angleDir, ChangedSkillData data)
    {
        Launcher = launcher;
        Target=target;
        angleDirection = angleDir.normalized;
        Data = data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        StartCoroutine(WrappingInvokeDelay(data.duration));
    }


  
    private void FixedUpdate()//물리처리
    {
        rb.velocity = angleDirection * Data.speed;
        StartCoroutine(AngleDirDelay());
        transform.Rotate(Vector3.forward, Data.rotateSpeed * Time.fixedDeltaTime); //프리팹 자체 회전
    }
    void OnTriggerEnter2D(Collider2D collision)//충돌했을 시
    {
            if (Target.layer==collision.gameObject.layer)
            {
                collision.GetComponent<BaseStat>().Damaged(Data.damage);
                StartCoroutine(WrappingInvokeDelay(0f));
            }
    }
    private IEnumerator WrappingInvokeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ProjectileObjectPool.Instance.Release(serialName, this.gameObject);
    }

    private IEnumerator AngleDirDelay()
    {
        yield return new WaitForSeconds(Data.hormingStartDelay);
        Vector2 self = this.transform.position;
        Vector2 dirNormal = (Target.transform.position - Launcher.transform.position).normalized;
        angleDirection = Vector2.Lerp(angleDirection, dirNormal, Time.deltaTime * Data.hormingTurnDelay);
        rb.velocity = angleDirection * Data.speed;
    }
}
