using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    private ChangedSkillData data;//ÎπÑÏñ¥?àÎäî ?¨ÏÇ¨Ï≤¥Ïùò ?∞Ïù¥??
    private Vector2 Target;//ÎπÑÏñ¥?àÎäî Í≥µÍ≤©Î∞©Ìñ•
    private Vector2 angleDirection;//ÎπÑÏñ¥?àÎäî Ï∂îÏ†Å Í≥µÍ≤©Î∞©Ìñ•
    private Rigidbody2D rb;//?ÑÎ¶¨?πÌïú ?¨ÏÇ¨Ï≤¥Ïùò Î¶¨ÏßìÎ∞îÎîî
    public string serialName;//?¥Î¶Ñ
    public void Init(Vector2 target, Vector2 angleDir, ChangedSkillData _data)
    {
        Target = target;
        angleDirection = angleDir.normalized;
        data = _data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        StartCoroutine(WrappingInvokeDelay(data.duration));
    }

    private void FixedUpdate()//π∞∏Æ√≥∏Æ
    {
        rb.velocity = angleDirection * data.speed;
        StartCoroutine(AngleDirDelay());
        transform.Rotate(Vector3.forward, data.rotateSpeed * Time.fixedDeltaTime); //«¡∏Æ∆’ ¿⁄√º »∏¿¸
    }

    void OnTriggerEnter2D(Collider2D collision)//√Êµπ«ﬂ¿ª Ω√
    {
        if (collision.GetComponent<EnemyStateMachine>() != null) //&&Target.tag==collsion.tag or layermask ∫Ò±≥
        {
            collision.GetComponent<BaseStat>().Damaged(data.damage);
            StartCoroutine(WrappingInvokeDelay(0f));//skillShooter??ReturnToPool() Î©îÏÑú?úÎ? ?∏Ï∂ú
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
