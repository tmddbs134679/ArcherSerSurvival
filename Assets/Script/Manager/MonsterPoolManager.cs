using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : MonoBehaviour
{
    public GameObject objectPrefab;

    [SerializeField]
    private int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();


    void Start()
    {
        // 풀에 오브젝트 미리 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);  // 오브젝트 비활성화
            pool.Enqueue(obj);  // 큐에 넣기
        }
    }


    // 풀에서 오브젝트를 가져오는 함수
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();  // 큐에서 오브젝트 하나 꺼내기
            obj.SetActive(true);  // 오브젝트 활성화
            return obj;
        }
        else
        {
            // 필요시 풀에 오브젝트를 동적으로 추가
            GameObject obj = Instantiate(objectPrefab);
            return obj;
        }
    }

    // 사용이 끝난 오브젝트를 풀에 반환하는 함수
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  // 오브젝트 비활성화
        pool.Enqueue(obj);  // 풀에 반환
    }
}
