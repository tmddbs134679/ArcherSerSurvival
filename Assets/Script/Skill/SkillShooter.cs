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
    private Stack<GameObject> gameObjects = new Stack<GameObject>();//투사체들을 담을 스택


    public Transform target;

    public Transform pivot;
    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            StartCoroutine(FireWithDelay());
            fireTimer = 0;
        }
    }

    private void Fire(int count)
    {
        GameObject projectile; //투사체프리팹과 투사체 데이터를 받을 오브젝트
        if (gameObjects.Count > 0)//스택안에 오브젝트가 남아있으면
        {
            projectile = gameObjects.Pop();//오브젝트를 꺼낸다.
            projectile.transform.position = pivot.position;
            projectile.transform.rotation = Quaternion.identity;
            projectile.SetActive(true);
        }
        else
        {
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);//투사체 프리팹을 받음
            projectile.GetComponent<ProjectileController2>().OnClick += ReturnToPool;//skillShooter를 구독해서 ReturnToPool을 쓸 수 있게함
        }
        Vector2 dir = target.position - pivot.position;
        Vector2 angleDir = Quaternion.Euler(0, 0, -(count * Data.angle / 2) + Data.angle * (count - 1)) * dir;
        projectile.GetComponent<ProjectileController2>().Init(dir,angleDir, Data);//공격방향,데이터를 받음
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
            Fire(i);
            yield return new WaitForSeconds(individualFireRate); // 각 발사마다 individualFireRate초 만큼 딜레이
        }
    }

}
