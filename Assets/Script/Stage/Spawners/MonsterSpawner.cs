using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private string monsterID;         
    [SerializeField] private int spawnMonsterCount;      //?앹꽦??紐ъ뒪????
    
    private MonsterSpawnPoint spawnPoint;
    private MonsterPoolManager poolManager;


    private void Start()
    {
        spawnPoint = GetComponent<MonsterSpawnPoint>();
        poolManager = MonsterPoolManager.Instance;
        SpawnWave();
    }

    private void SpawnWave()
    {   //紐ъ뒪???꾨? ?앹꽦
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
