using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ExplosionSkill : MonoBehaviour
{
    public GameObject projectilePrefab; //투사체 프리팹
    public ProjectileData Data; //투사체의 데이터
    public float fireRate; //한 사이클 발사 간격
    public float individualFireRate;//개별 발사간격
    private float fireTimer;//단순 시간변수
    //파티클

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
        GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name); //objectpool에서 자동으로 부족할 시 프리팹을 채워줌

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

//player,monster를 unit으로 상속받아서 공통된 변수를 써야함
//타겟의 레이어or태그를 받아서 투사체의 충돌 처리를 구별해 줘야함 
//projectile의 OnTriggerEnter2D메서드에서 정의 필요
            var currentPivotPos = player.transform.position;
            var targetTransform = player.GetComponent<PlayerController>().GetClosestEnemy();
            if (targetTransform == null) yield break;
            var currentTargetPos = targetTransform.position;

            Fire(i, currentPivotPos, currentTargetPos);
            yield return new WaitForSeconds(individualFireRate);

        }
    }


}
