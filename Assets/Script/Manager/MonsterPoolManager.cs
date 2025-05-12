using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : Singleton<MonsterPoolManager>
{
    public GameObject[] objectPrefabs;

    [SerializeField]
    private int poolSize = 10;

    //private Queue<GameObject> pool = new Queue<GameObject>();
    private Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();

    GameObject poolParent;


    protected override void Awake()
    {
        base.Awake();

        pool[0] = new Queue<GameObject>();
        pool[1] = new Queue<GameObject>();
        pool[2] = new Queue<GameObject>();

    }

    void Start()
    {
        // Ǯ�� ������Ʈ �̸� ����
        for (int j = 0; j < objectPrefabs.Length; j++)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectPrefabs[j]);
                obj.GetComponent<Health>().OnDie += () => ReturnObject(obj, j);
                obj.SetActive(false);  // ������Ʈ ��Ȱ��ȭ
                obj.transform.SetParent(gameObject.transform);
                pool[j].Enqueue(obj);  // ť�� �ֱ�
            }
        }
    }


    // Ǯ���� ������Ʈ�� �������� �Լ�
    public GameObject GetObject(int index)
    {
        GameManager.Instance.EnemyCounting(1);
        if (pool[index].Count > 0)
        {
            GameObject obj = pool[index].Dequeue();  // ť���� ������Ʈ �ϳ� ������
            obj.SetActive(true);  // ������Ʈ Ȱ��ȭ
            return obj;
        }
        else
        {
            // �ʿ�� Ǯ�� ������Ʈ�� �������� �߰�
            GameObject obj = Instantiate(objectPrefabs[index]);
            return obj;
        }
    }

    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �Լ�
    public void ReturnObject(GameObject obj, int index)
    {
        GameManager.Instance.EnemyCounting(-1);
        obj.SetActive(false);  // ������Ʈ ��Ȱ��ȭ
        pool[index].Enqueue(obj);  // Ǯ�� ��ȯ
    }
}
