using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int stage = 0;
    [SerializeField]
    private float time;
    [SerializeField]
    private bool isOpen = false;

    public GameObject[] entities;

    [SerializeField]
    private int enemyCount = 0;

    public MonsterPoolManager monsterPool;

    public GameObject[] rooms;


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
        monsterPool.ReturnObject(monster, int.Parse(monster.name));
        CheckEnemy();
    }


    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Init_GameManager();
    }



    private void Start()
    {

    }

    private void Init_GameManager()
    {
        CreateRoom();
    }

    void Update()
    {
        //추후 사용할수도있는 시간 미리체크
        time += Time.deltaTime;
    }

   
    public void CreateRoom()
    {
        Instantiate(rooms[UnityEngine.Random.RandomRange(0, rooms.Length)]);
    }
    public void NextRoom()
    {
        if (isOpen)
        {
            SceneManager.LoadScene("Main");
        }
    }


    public static event Action openCloseDoor;
    public void CheckEnemy()
    {

        if (enemyCount <= 0)
        {
            isOpen = true;
            openCloseDoor?.Invoke();
        }

    }

    public void EnemyCounting()
    {
        entities = GameObject.FindGameObjectsWithTag("Enemy");

        enemyCount = entities.Length;
    }

    public void spawn()
    {
        GameObject monster = monsterPool.GetObject(UnityEngine.Random.RandomRange(0, 3));
        monster.transform.position = new Vector3(UnityEngine.Random.RandomRange(0,10), UnityEngine.Random.RandomRange(0, 10),0);
    }

}
