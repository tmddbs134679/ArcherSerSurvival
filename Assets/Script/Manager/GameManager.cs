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
    [SerializeField]
    private int roomCount = 1;
    public GameObject[] rooms;
    public GameObject[] bossRooms;
    public SkillLevelSystem skillLevelSystem;
    private void HandleMonsterDeath(GameObject monster)
    {
        enemyCount--;
        MonsterPoolManager.Instance.ReturnObject(monster, int.Parse(monster.name));
        //CheckEnemy();
    }
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameManager Awake");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isOpen = false;
        Init_GameManager();
    }
    private void Init_GameManager()
    {
        CreateRoom();
    }
    void Update()
    {
        time += Time.deltaTime;
    }
    public void CreateRoom()
    {
        Debug.Log("Create Room");
        if (roomCount % 5 == 0)
        {
            Instantiate(bossRooms[UnityEngine.Random.RandomRange(0, bossRooms.Length)]);
        }
        else
        {
            Instantiate(rooms[UnityEngine.Random.RandomRange(0, rooms.Length)]);
        }
        roomCount++;
    }
    public void NextRoom()
    {
        if (isOpen)
        {
            isOpen = false;
            PlayerController.Instance.transform.position = new Vector3(0, 0, 0);
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
            //UIManager.Instance.ShowUI("Reward");
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
}







