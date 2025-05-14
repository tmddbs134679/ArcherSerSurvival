using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ChangedSkillData Data;//??쑴堉??덈뮉 ??沅쀯㎗?곸벥 ?怨쀬뵠??
    private GameObject Target;//?⑤벀爰????
    public GameObject Launcher;//獄쏆뮇沅쀯㎗?
    private Vector2 angleDirection;//??쑴堉??덈뮉 ?곕뗄???⑤벀爰썼쳸?븍샨
    private Rigidbody2D rb;//?袁ⓥ봺?諭곷립 ??沅쀯㎗?곸벥 ?귐딆췂獄쏅뗀逾?
    public string serialName;//??已?

private bool justReflected = false;
    public void Init(GameObject launcher, GameObject target, Vector2 angleDir, ChangedSkillData data)
    {
        Launcher = launcher;
        Target = target;
        angleDirection = angleDir.normalized;
        Data = data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        StartCoroutine(WrappingInvokeDelay(data.duration));
    }

    private void FixedUpdate()//?얠눖?곻㎗?롡봺
    {
        Vector2 dir = (Target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle-90);

        StartCoroutine(AngleDirDelay());
      transform.Rotate(Vector3.forward, Data.rotateSpeed * Time.fixedDeltaTime); //?袁ⓥ봺???癒?퍥 ???읈
    }
    void OnTriggerEnter2D(Collider2D collision)//?겸뫖猷??됱뱽 ??
    {

        if (Target.layer == collision.gameObject.layer)
        {
            collision.GetComponent<BaseStat>().Damaged(Data.damage);
            StartCoroutine(WrappingInvokeDelay(0f));
        }


    }
    IEnumerator ResetReflectFlag()
{
    yield return new WaitForFixedUpdate();
    justReflected = false;
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
