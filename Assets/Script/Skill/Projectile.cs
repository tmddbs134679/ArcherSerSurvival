using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ChangedSkillData Data;//???닷젆???덈츎 ??亦낆?럸?怨몃꺄 ??⑥щ턄??
    private GameObject Target;//??ㅻ??????
    public GameObject Launcher;//?꾩룇裕뉑쾮??럸?
    private Vector2 angleDirection;//???닷젆???덈츎 ?怨뺣뾼????ㅻ??곗띁爾?釉띿깿
    private Rigidbody2D rb;//?熬곣뱿遊?獄?낮由???亦낆?럸?怨몃꺄 ?洹먮봿痍귞뛾?낅???
    public string serialName;//???藥?

    private bool justReflected = false;
    public void Init(GameObject launcher, GameObject target, Vector2 angleDir, ChangedSkillData data)
    {
        Launcher = launcher;
        Target = target;
        angleDirection = angleDir.normalized;
        Data = data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        float angle = Mathf.Atan2(angleDirection.y,angleDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        StartCoroutine(WrappingInvokeDelay(data.duration));
    }

    private void Update()//??좊닑?怨삠럸?濡〓뉴
    {
        if (Data != null)
        {
            if (!justReflected)
            {

                rb.velocity = angleDirection * Data.speed;

            }
            transform.Rotate(Vector3.forward, Data.rotateSpeed * Time.fixedDeltaTime); //?熬곣뱿遊????????????
        }
    }
    void OnTriggerEnter2D(Collider2D collision)//?寃몃쳳????깅굵 ??
    {

        if (Target.layer == collision.gameObject.layer)
        {
            collision.GetComponent<BaseStat>().Damaged(Data.damage);

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")||Target.layer == collision.gameObject.layer)
        {
            Vector2 incoming = rb.velocity.normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, incoming, 1.5f, LayerMask.GetMask("Wall"));
            // RaycastHit2D hit = Physics2D.Raycast(transform.position, incoming, Data.speed * Time.fixedDeltaTime *0.1f, LayerMask.GetMask("Wall"));

            if (hit.collider != null)
            {
                Vector2 normal = hit.normal;
                Vector2 reflect = Vector2.Reflect(incoming, normal);
                angleDirection = reflect.normalized;
                    // ?뚯쟾媛??명똿
    float angle = Mathf.Atan2(angleDirection.y, angleDirection.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, angle - 90);

                justReflected = true;
                StartCoroutine(ResetReflectFlag());
            }
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
