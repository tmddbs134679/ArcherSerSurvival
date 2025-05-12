using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ExplosionSkill : MonoBehaviour
{
    public GameObject projectilePrefab; //?ъ궗泥??꾨━??
    public ChangedSkillData Data; //?ъ궗泥댁쓽 ?곗씠??
    public float fireRate; //???ъ씠??諛쒖궗 媛꾧꺽
    public float individualFireRate;//媛쒕퀎 諛쒖궗媛꾧꺽
    private float fireTimer;//?⑥닚 ?쒓컙蹂??
    //?뚰떚??

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
            var targetTransform = player.GetComponent<PlayerTargeting>().GetClosestEnemy();
            if (targetTransform == null) yield break;
            var currentTargetPos = targetTransform.position;

            Fire(i, currentPivotPos, currentTargetPos);
            yield return new WaitForSeconds(individualFireRate);

        }
    }


}
