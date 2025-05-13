using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    private ChangedSkillData data;//鍮꾩뼱?덈뒗 ?ъ궗泥댁쓽 ?곗씠??
    private Vector2 Target;//鍮꾩뼱?덈뒗 怨듦꺽諛⑺뼢
    private Vector2 angleDirection;//鍮꾩뼱?덈뒗 異붿쟻 怨듦꺽諛⑺뼢
    private Rigidbody2D rb;//?꾨━?뱁븳 ?ъ궗泥댁쓽 由ъ쭞諛붾뵒
    public string serialName;//?대쫫
    public GameObject ownerObject;

    public void Init(GameObject tempobject, Vector2 target, Vector2 angleDir, ChangedSkillData _data)
    {
        ownerObject = tempobject;
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
        //에네미가 발사했을때
        //6 == enemy
        if (ownerObject.layer == 6)
        {
            if (collision.gameObject.layer == 3)
            {
                collision.GetComponent<BaseStat>().Damaged(data.damage);
                StartCoroutine(WrappingInvokeDelay(0f));//skillShooter??ReturnToPool() 硫붿꽌?쒕? ?몄텧
            }
        }
        //플레이어가 발사했을때
        else if (ownerObject.layer == 3)
        {
            if (collision.gameObject.layer == 6)
            {
                collision.GetComponent<BaseStat>().Damaged(data.damage);
                StartCoroutine(WrappingInvokeDelay(0f));//skillShooter??ReturnToPool() 硫붿꽌?쒕? ?몄텧
            }
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
