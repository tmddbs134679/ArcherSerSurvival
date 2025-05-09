using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    //[SerializeField] private int monsterID;
    [SerializeField] private int monsterCount;      //생성할 몬스터 수
    
    private MonsterSpawnPoint spawnPoint;
    private MonsterPoolManager poolManager;


    private void Start()
    {
        spawnPoint = GetComponent<MonsterSpawnPoint>();
        poolManager = GetComponent<MonsterPoolManager>();
    }

    private void SpawnWave()
    {
        for(int i = 0; i < monsterCount; i++) 
        {
            GameObject monster = poolManager.GetObject();
            if(monster != null) 
            {
                monster.transform.position = spawnPoint.GetRandomPoint();
            }
        }
    }

   
}
