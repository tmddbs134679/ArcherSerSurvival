using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpawner : MonoBehaviour
{
    private List<GameObject> enemyList = new List<GameObject>(); //??밴쉐???怨룸굶???????귐딅뮞??
    private int wave;   //?袁⑹삺 ??μ뵠??
    private const float maxDistance = 20.0f;    //???쟿??곷선?? ?怨몄벥 筌ㅼ뮆? 椰꾧퀡??
    private Transform player;
    private MonsterPoolManager monsterPool;

    [SerializeField]
    private float spawnDelay = 5f;
    private float timer = 0f;
    private int levelUpThreshold = 2;
    private int killCount = 0;

    private void Start()
    {
        player = PlayerController.Instance.transform;
        if (player == null)
        {
            Debug.LogWarning(gameObject.name + ": Player Transform Not Found");
            Destroy(gameObject);
        }
        monsterPool = MonsterPoolManager.Instance;
        if (monsterPool == null)
        {
            Debug.LogWarning(gameObject.name + ": MonsterPool Not Found");
            Destroy(gameObject);
        }
        SpawnWave();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnDelay)
        {
            SpawnWave();
            timer = 0;
        }
        if(killCount >= levelUpThreshold)
        {
            levelUpThreshold += killCount;
            UIManager.Instance.ShowUI("Reward");
            killCount = 0;
        }

        foreach(GameObject enemy in enemyList)
        {
            Vector3 distance = player.position - enemy.transform.position;
            if (distance.magnitude >= maxDistance) //???쟿??곷선?? ??댭?筌렺??곸１??野껋럩??
            {
                Reposition(enemy); //??媛숂㎉?
            }
        }
    }

    private void SpawnWave()
    {

        wave++;

        int spawnCount = Mathf.RoundToInt(wave * 1.5f);

        for(int i = 0; i < spawnCount; i++) 
        {
            CreateRandomEnemy();
        }
        if (wave % 10 == 0)
        {
            CreateBoss();
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
            enemy.GetComponent<EnemyStat>().OnDie += () => killCount++;
        }
    }

    private void CreateBoss()
    {
        GameObject enemy = monsterPool.GetRandomBossObject();
        Reposition(enemy);
        enemyList.Add(enemy);

        EnemyStat enemyStat = enemy.GetComponent<EnemyStat>();
        if (enemyStat != null)
        {
            enemy.GetComponent<EnemyStat>().OnDie += () => enemyList.Remove(enemy);
        }
    }

    void Reposition(GameObject enemy)
    {
        player = PlayerController.Instance.transform;
       
        float randomPosX = (Random.value > 0.5f) ? Random.Range(-5.0f, -3.0f) : Random.Range(3.0f, 5.0f);
        float randomPosY = (Random.value > 0.5f) ? Random.Range(-5.0f, -3.0f) : Random.Range(3.0f, 5.0f);
        //???쟿??곷선 域뱀눘荑???뺣쑁 ?袁⑺뒄?癒?퐣 ??밴쉐
        Vector2 newPos =
        (Vector2)player.position + new Vector2(randomPosX, randomPosY);

        enemy.transform.position = newPos;
    }
}
