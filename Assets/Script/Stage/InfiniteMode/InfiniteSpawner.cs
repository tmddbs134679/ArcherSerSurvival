using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;     //?앹꽦?????꾨━??
    
    private List<GameObject> enemyList = new List<GameObject>(); //?앹꽦???곷뱾???대뒗 由ъ뒪??
    private int wave;   //?꾩옱 ?⑥씠釉?
    private const float maxDistance = 20.0f;    //?뚮젅?댁뼱? ?곸쓽 理쒕? 嫄곕━
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
            if (distance.magnitude >= maxDistance) //?뚮젅?댁뼱? ?덈Т 硫?댁죱??寃쎌슦
            {
                Reposition(enemy); //?щ같移?
            }
        }
    }

    private void SpawnWave()
    {
        //Reward 泥섎━
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
        GameObject enemy = monsterPool.GetRandomObject();
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
        //?뚮젅?댁뼱 洹쇱쿂 ?쒕뜡 ?꾩튂?먯꽌 ?앹꽦
        Vector2 newPos =
        (Vector2)player.position + new Vector2(randomPosX, randomPosY);

        enemy.transform.position = newPos;
    }
}
