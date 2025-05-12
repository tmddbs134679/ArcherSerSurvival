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
        // 풀에 오브젝트 미리 생성
        for (int j = 0; j < objectPrefabs.Length; j++)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectPrefabs[j]);
                obj.GetComponent<Health>().OnDie += () => ReturnObject(obj, j);
                obj.SetActive(false);  // 오브젝트 비활성화
                obj.transform.SetParent(gameObject.transform);
                pool[j].Enqueue(obj);  // 큐에 넣기
            }
        }
    }


    // 풀에서 오브젝트를 가져오는 함수
    public GameObject GetObject(int index)
    {
        GameManager.Instance.EnemyCounting(1);
        if (pool[index].Count > 0)
        {
            GameObject obj = pool[index].Dequeue();  // 큐에서 오브젝트 하나 꺼내기
            obj.SetActive(true);  // 오브젝트 활성화
            return obj;
        }
        else
        {
            // 필요시 풀에 오브젝트를 동적으로 추가
            GameObject obj = Instantiate(objectPrefabs[index]);
            return obj;
        }
    }

    // 사용이 끝난 오브젝트를 풀에 반환하는 함수
    public void ReturnObject(GameObject obj, int index)
    {
        GameManager.Instance.EnemyCounting(-1);
        obj.SetActive(false);  // 오브젝트 비활성화
        pool[index].Enqueue(obj);  // 풀에 반환
    }
}
