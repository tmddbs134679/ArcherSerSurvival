using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;     //생성할 적 프리팹
    
    private List<GameObject> enemyList = new List<GameObject>(); //생성된 적들을 담는 리스트
    private int wave;   //현재 웨이브
    private const float maxDistance = 20.0f;    //플레이어와 적의 최대 거리
    private Transform player;
    private MonsterPoolManager monsterPool;

    private void Start()
    {
        player = PlayerController.Instance.transform;
        if (player == null)
        {
            Debug.LogWarning(gameObject.name + ": Player Transform Not Found");
            Destroy(gameObject);
        }
        wave = 100;
        monsterPool = MonsterPoolManager.Instance;
        if (monsterPool == null)
        {
            Debug.LogWarning(gameObject.name + ": MonsterPool Not Found");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(enemyList.Count <= 0)
        {
            SpawnWave();
        }

        foreach(GameObject enemy in enemyList)
        {
            Vector3 distance = player.position - enemy.transform.position;
            if (distance.magnitude >= maxDistance) //플레이어와 너무 멀어졌을 경우
            {
                Reposition(enemy); //재배치
            }
        }
    }

    private void SpawnWave()
    {
        //Reward 처리
        UIManager.Instance?.ShowUI("RewardUI");

        wave++;

        int spawnCount = Mathf.RoundToInt(wave * 1.5f);

        for(int i = 0; i < spawnCount; i++) 
        {
            CreateRandomEnemy();
        }
    }

    private void CreateRandomEnemy()
    {
        GameObject enemy = monsterPool.GetObject(Random.Range(0, 5));
        Reposition(enemy);   
        enemyList.Add(enemy);

        EnemyStat enemyStat = enemy.GetComponent<EnemyStat>(); 
        if(enemyStat != null ) 
        {
            enemy.GetComponent<EnemyStat>().OnDie += () => enemyList.Remove(enemy);
        }
        
    }
    void Reposition(GameObject enemy)
    {
        player = PlayerController.Instance.transform;
       
        float randomPosX = (Random.value > 0.5f) ? Random.Range(-5.0f, -3.0f) : Random.Range(3.0f, 5.0f);
        float randomPosY = (Random.value > 0.5f) ? Random.Range(-5.0f, -3.0f) : Random.Range(3.0f, 5.0f);
        //플레이어 근처 랜덤 위치에서 생성
        Vector2 newPos =
        (Vector2)player.position + new Vector2(randomPosX, randomPosY);

        enemy.transform.position = newPos;
    }
}
