using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private int stage = 0;
    [SerializeField]
    private float time;
    [SerializeField]
    public bool isOpen = false;
    public GameObject[] entities;
    [SerializeField]
    private int enemyCount = 0;
    [SerializeField]
    private int roomCount = 1;
    public GameObject[] rooms;
    public GameObject[] bossRooms;
    public GameObject lodingPrefab;
    public GameObject lodingObject;
    public SkillLevelSystem skillLevelSystem;

    public bool isOption = false;
    public bool isStartLoading = false;






    private void HandleMonsterDeath(GameObject monster)
    {
        enemyCount--;
        MonsterPoolManager.Instance.ReturnObject(monster, int.Parse(monster.name));
        //CheckEnemy();
    }
    protected override void Awake()
    {
        base.Awake();
        if (GameManager.Instance == this)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            rooms = Resources.LoadAll<GameObject>("Prefabs/Stages/Room");
            bossRooms = Resources.LoadAll<GameObject>("Prefabs/Stages/BossRoom");

            lodingObject = GameObject.Find("Loading");
            
            //디버그
            //코루틴
            //인보크
            
        }
        
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        lodingObject = GameObject.Find("Loading");
        isOpen = false;
        Init_GameManager();




        Invoke("DelayFadeOut", 0.5f);

        

    }

    public void DelayFadeOut()
    {
        Debug.Log("??");
        UIManager.Instance.FadeOutUI("Loading");
        /*
        foreach (GameObject obj in UIManager.Instance.uiObjects)
        {
            if (obj.name == "Loading")
            {
                Debug.Log("??");
                UIManager.Instance.FadeOutUI("Loading");
                return;
            }
        }
        
        Invoke("DelayFadeOut", 0.5f);
        */
    }
    private void Init_GameManager()
    {
        CreateRoom();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOption();
        }
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
            isStartLoading = true;
            UIManager.Instance.FadeInUI("Loading");
            
        }
    }

    // LodingFadeIn => NextSceneLoad
    public void NextSceneLoad()
    {
        //LoadingManager.LoadScene("AITestScene");
        PlayerController.Instance.transform.position = new Vector3(0,0,0);
        SceneManager.LoadScene("AITestScene");
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
    public void GameOver()
    {
        UIManager.Instance.ShowUI("GameOverUI");
    }

    public void OnOption()
    {

        if (isOption == false)
        {
            isOption = true;
            Time.timeScale = 0f;
            UIManager.Instance.ShowUI("Option");
        }

        else
        {
            isOption = false;
            Time.timeScale = 1.0f;
            UIManager.Instance.HideUI("Option");
        }


    }


    public void LoadSceneLobby()
    {
        SceneManager.LoadScene("Lobby");
    }


}
