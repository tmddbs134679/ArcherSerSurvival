using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ExplosionSkill : MonoBehaviour
{
    public GameObject projectilePrefab; //?¨ÏÇ¨Ï≤??ÑÎ¶¨??
    public ChangedSkillData Data; //?¨ÏÇ¨Ï≤¥Ïùò ?∞Ïù¥??
    public float fireRate; //???¨Ïù¥??Î∞úÏÇ¨ Í∞ÑÍ≤©
    public float individualFireRate;//Í∞úÎ≥Ñ Î∞úÏÇ¨Í∞ÑÍ≤©
    private float fireTimer;//?®Ïàú ?úÍ∞ÑÎ≥Ä??
    //?åÌã∞??

    public GameObject player;

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            StartCoroutine(FireWithDelay());
            fireTimer = 0;
        }

    }

    private void Fire(int count, Vector2 pivotPos, Vector2 targetPos)
    {
        GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name); //objectpoolø°º≠ ¿⁄µø¿∏∑Œ ∫Œ¡∑«“ Ω√ «¡∏Æ∆’¿ª √§øˆ¡‹

        projectile.transform.position = pivotPos;
        projectile.transform.rotation = Quaternion.identity;

        Vector2 dir = targetPos - pivotPos;
        Vector2 angleDir = Quaternion.Euler(0, 0, -(Data.angle * Data.count / 2f) + Data.angle * count) * dir;

        projectile.GetComponent<Projectile>().Init(targetPos, angleDir, Data);
    }

    private IEnumerator FireWithDelay()
    {
        for (int i = 0; i < Data.count; i++)
        {

//player,monster∏¶ unit¿∏∑Œ ªÛº”πﬁæ∆º≠ ∞¯≈Îµ» ∫Øºˆ∏¶ Ω·æﬂ«‘
//≈∏∞Ÿ¿« ∑π¿ÃæÓor≈¬±◊∏¶ πﬁæ∆º≠ ≈ıªÁ√º¿« √Êµπ √≥∏Æ∏¶ ±∏∫∞«ÿ ¡‡æﬂ«‘ 
//projectile¿« OnTriggerEnter2D∏ﬁº≠µÂø°º≠ ¡§¿« « ø‰
            var currentPivotPos = player.transform.position;
            var targetTransform = player.GetComponent<PlayerController>().GetClosestEnemy();
            if (targetTransform == null) yield break;
            var currentTargetPos = targetTransform.position;

            Fire(i, currentPivotPos, currentTargetPos);
            yield return new WaitForSeconds(individualFireRate);

        }
    }


}
