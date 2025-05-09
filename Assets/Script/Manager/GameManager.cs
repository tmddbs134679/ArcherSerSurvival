using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public GameManager manager;
    public static GameManager instance;

    [SerializeField]
    private int stage = 0;
    [SerializeField]
    private float time;
    [SerializeField]
    private bool isOpen = false;

    GameObject[] entities;

    [SerializeField]
    private int enemyCount = 0;

    public MonsterPoolManager monsterPool;


    private void OnEnable()
    {
        Monster.OnMonsterDeath += HandleMonsterDeath;
    }

    private void OnDisable()
    {
        Monster.OnMonsterDeath -= HandleMonsterDeath;
    }

    // 몬스터가 죽었을 때 풀에 반환하는 함수
    private void HandleMonsterDeath(GameObject monster)
    {
        enemyCount--;
        monsterPool.ReturnObject(monster);
        CheckEnemy();
    }

    //딱히 할게없구나
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            entities = GameObject.FindGameObjectsWithTag("Enemy");
        }

    }

    void Update()
    {
        //추후 사용할수도있는 시간 미리체크
        time += Time.deltaTime;
    }



    public void CheckEnemy()
    {

        if (enemyCount <= 0)
        {
            isOpen = true;
        }

    }

    public void EnemyCounting()
    {
        entities = GameObject.FindGameObjectsWithTag("Enemy");

        enemyCount = entities.Length;
    }

    public void spawn()
    {
        GameObject monster = monsterPool.GetObject();
        monster.transform.position = new Vector3();
    }

}
