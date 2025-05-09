using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private int monsterID;         
    [SerializeField] private int spawnMonsterCount;      //������ ���� ��
    
    private MonsterSpawnPoint spawnPoint;
    private MonsterPoolManager poolManager;


    private void Start()
    {
        spawnPoint = GetComponent<MonsterSpawnPoint>();
        poolManager = MonsterPoolManager.Instance;
        SpawnWave();
    }

    private void SpawnWave()
    {   //���� ���� ����
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
