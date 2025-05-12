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

    public SkillLevelSystem skillLevelSystem;
    //?ъ븘??二쇱꽍 ?뚯뒪??
    private void OnEnable()
    {
        //Monster.OnMonsterDeath += HandleMonsterDeath;
    }

    private void OnDisable()
    {
        //Monster.OnMonsterDeath -= HandleMonsterDeath;
    }





    // 筌뤣딅뮞?怨? 雅뚯럩肉????????獄쏆꼹???롫뮉 ??λ땾
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
        //?곕???????醫롫땾?袁⑹뿳????볦퍢 沃섎챶?곻㎗?꾧쾿
        //?곕???????醫롫땾?袁⑹뿳????볦퍢 沃섎챶?곻㎗?꾧쾿
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
            SceneManager.LoadScene("AITestScene");
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