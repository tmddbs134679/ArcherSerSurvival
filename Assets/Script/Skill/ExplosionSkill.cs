using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ExplosionSkill : MonoBehaviour
{
    public GameObject projectilePrefab; //?¨ÏÇ¨Ï≤??ÑÎ¶¨??
    public ProjectileData Data; //?¨ÏÇ¨Ï≤¥Ïùò ?∞Ïù¥??
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
        GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name); //objectpool?êÏÑú ?êÎèô?ºÎ°ú Î∂ÄÏ°±Ìï† ???ÑÎ¶¨?πÏùÑ Ï±ÑÏõåÏ§?

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

//player,monsterÎ•?unit?ºÎ°ú ?ÅÏÜçÎ∞õÏïÑ??Í≥µÌÜµ??Î≥Ä?òÎ? ?®Ïïº??
//?ÄÍ≤üÏùò ?àÏù¥?¥or?úÍ∑∏Î•?Î∞õÏïÑ???¨ÏÇ¨Ï≤¥Ïùò Ï∂©Îèå Ï≤òÎ¶¨Î•?Íµ¨Î≥Ñ??Ï§òÏïº??
//projectile??OnTriggerEnter2DÎ©îÏÑú?úÏóê???ïÏùò ?ÑÏöî
            var currentPivotPos = player.transform.position;
            var targetTransform = player.GetComponent<PlayerController>().GetClosestEnemy();
            if (targetTransform == null) yield break;
            var currentTargetPos = targetTransform.position;

            Fire(i, currentPivotPos, currentTargetPos);
            yield return new WaitForSeconds(individualFireRate);

        }
    }


}
