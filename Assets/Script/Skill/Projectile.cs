using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ChangedSkillData Data;//????룹젂????덉툗 ??雅?굞?????⑤챶爰????Β????
    private GameObject Target;//?????????
    public GameObject Launcher;//?袁⑸즵獒뺣뎾苡????
    private Vector2 angleDirection;//????룹젂????덉툗 ??⑤베毓???????怨쀫쓠???됰씮源?
    private Rigidbody2D rb;//??ш끽諭욥걡?????뵳???雅?굞?????⑤챶爰??域밸Ŧ遊욜뿆洹욌쎗?????
    public string serialName;//?????

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


  
    private void FixedUpdate()//??醫딅땻??⑥궇??嚥▲볥돱
    {
        rb.velocity = angleDirection * Data.speed;
        StartCoroutine(AngleDirDelay());
        transform.Rotate(Vector3.forward, Data.rotateSpeed * Time.fixedDeltaTime); //??ш끽諭욥걡????????????
    }
    void OnTriggerEnter2D(Collider2D collision)//?野껊챶爾????源낃도 ??
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
