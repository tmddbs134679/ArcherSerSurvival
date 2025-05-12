using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    public GameObject[] rooms;

    //크아악 주석 테스트
    private void OnEnable()
    {
        //Monster.OnMonsterDeath += HandleMonsterDeath;
    }

    private void OnDisable()
    {
        //Monster.OnMonsterDeath -= HandleMonsterDeath;
    }





    // 紐ъ뒪?곌? 二쎌뿀???????諛섑솚?섎뒗 ?⑥닔
    private void HandleMonsterDeath(GameObject monster)
    {
        enemyCount--;
        MonsterPoolManager.Instance.ReturnObject(monster, int.Parse(monster.name));
        CheckEnemy();
    }


    protected override void Awake()
    {
        base.Awake();
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
        //異뷀썑 ?ъ슜?좎닔?꾩엳???쒓컙 誘몃━泥댄겕
        //異뷀썑 ?ъ슜?좎닔?꾩엳???쒓컙 誘몃━泥댄겕
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
            UIManager.Instance.ShowUI("Reward");
        }

    }

    public void EnemyCounting(int count)
    {
        /*
        entities = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = entities.Length;
        */
        enemyCount += count;
        CheckEnemy();
    }

    public void test_spawn()
    {
        GameObject monster = MonsterPoolManager.Instance.GetObject(UnityEngine.Random.RandomRange(0, 3));
        monster.transform.position = new Vector3(UnityEngine.Random.RandomRange(0, 10), UnityEngine.Random.RandomRange(0, 10), 0);
        EnemyCounting(1);
    }

    public void spawn(int index, Vector3 spawnPos)
    {
        GameObject monster = MonsterPoolManager.Instance.GetObject(index);
        monster.transform.position = spawnPos;
        EnemyCounting(1);
    }

}