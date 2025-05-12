using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ExplosionSkill : MonoBehaviour
{
    public GameObject projectilePrefab; //?�사�??�리??
    public ProjectileData Data; //?�사체의 ?�이??
    public float fireRate; //???�이??발사 간격
    public float individualFireRate;//개별 발사간격
    private float fireTimer;//?�순 ?�간변??
    //?�티??

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
        GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name); //objectpool?�서 ?�동?�로 부족할 ???�리?�을 채워�?

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

//player,monster�?unit?�로 ?�속받아??공통??변?��? ?�야??
//?�겟의 ?�이?�or?�그�?받아???�사체의 충돌 처리�?구별??줘야??
//projectile??OnTriggerEnter2D메서?�에???�의 ?�요
            var currentPivotPos = player.transform.position;
            var targetTransform = player.GetComponent<PlayerController>().GetClosestEnemy();
            if (targetTransform == null) yield break;
            var currentTargetPos = targetTransform.position;

            Fire(i, currentPivotPos, currentTargetPos);
            yield return new WaitForSeconds(individualFireRate);

        }
    }


}
