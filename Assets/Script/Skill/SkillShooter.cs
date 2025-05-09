using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class SkillShooter : MonoBehaviour
{
    public GameObject projectilePrefab; //투사체 프리팹
    public ProjectileData Data; //투사체의 데이터
    public float fireRate; //한 사이클 발사 간격
    public float individualFireRate;//개별 발사간격
    private float fireTimer;//단순 시간변수
    private Stack<GameObject> gameObjects;//투사체들을 담을 스택


    Transform target;

    Transform pivot;

    public GameObject player;

    void Awake()
    {
        gameObjects = new Stack<GameObject>(Data.count);
    }
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
        GameObject projectile;

        if (gameObjects.Count > 0)
        {
            projectile = gameObjects.Pop();
            projectile.transform.position = pivotPos;
            projectile.transform.rotation = Quaternion.identity;
            projectile.SetActive(true);
        }
        else
        {
            projectile = Instantiate(projectilePrefab, pivotPos, Quaternion.identity);
            projectile.GetComponent<ProjectileController2>().OnClick += ReturnToPool;
        }

        Vector2 dir = targetPos - pivotPos;
        Vector2 angleDir = Quaternion.Euler(0, 0, -(Data.angle * Data.count / 2f) + Data.angle * count) * dir;

        projectile.GetComponent<ProjectileController2>().Init(dir, angleDir, Data);
    }

    public void ReturnToPool(GameObject projectile)//투사체가 소멸해야 할 때 호출
    {
        projectile.SetActive(false);
        gameObjects.Push(projectile);

    }

private IEnumerator FireWithDelay()
{
    for (int i = 0; i < Data.count; i++)
    {
        var currentPivotPos = player.transform.position;
        var targetTransform = player.GetComponent<PlayerController>().GetClosestEnemy();
        if (targetTransform == null) yield break;
        var currentTargetPos = targetTransform.position;

        Fire(i, currentPivotPos, currentTargetPos);

        yield return new WaitForSeconds(individualFireRate);
    }
}


}
