using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameManager manager;
    public static GameManager instance;

    [SerializeField]
    private int stage = 0;
    [SerializeField]
    private float time;
    [SerializeField]
    private bool isOpen = false;

    GameObject[] entities;

    [SerializeField]
    private int enemyCount = 0;

    public MonsterPoolManager monsterPool;


    private void OnEnable()
    {
        Monster.OnMonsterDeath += HandleMonsterDeath;
    }

    private void OnDisable()
    {
        Monster.OnMonsterDeath -= HandleMonsterDeath;
    }

    // ���Ͱ� �׾��� �� Ǯ�� ��ȯ�ϴ� �Լ�
    private void HandleMonsterDeath(GameObject monster)
    {
        enemyCount--;
        monsterPool.ReturnObject(monster);
        CheckEnemy();
    }

    //�̱��� ����
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //����ȯ�� ���ı��� ������ �غ����ҵ�
        }
    }


    //���� �ҰԾ�����
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            entities = GameObject.FindGameObjectsWithTag("Enemy");
        }

    }

    void Update()
    {
        //���� ����Ҽ����ִ� �ð� �̸�üũ
        time += Time.deltaTime;
    }



    public void CheckEnemy()
    {

        if (enemyCount <= 0)
        {
            isOpen = true;
        }

    }

    public void EnemyCounting()
    {
        entities = GameObject.FindGameObjectsWithTag("Enemy");

        enemyCount = entities.Length;
    }

    public void spawn()
    {
        GameObject monster = monsterPool.GetObject();
        monster.transform.position = new Vector3();
    }

}
