using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private int monsterID;         
    [SerializeField] private int spawnMonsterCount;      //생성할 몬스터 수
    
    private MonsterSpawnPoint spawnPoint;
    private MonsterPoolManager poolManager;


    private void Start()
    {
        spawnPoint = GetComponent<MonsterSpawnPoint>();
        poolManager = MonsterPoolManager.Instance;
        SpawnWave();
    }

    private void SpawnWave()
    {   //몬스터 전부 생성
        for(int i = 0; i < spawnMonsterCount; i++) 
        {
            GameObject monster = poolManager.GetObject(monsterID);
            if(monster != null) 
            {
                monster.transform.position = spawnPoint.GetRandomPoint();
            }
        }
    }
}
