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
        // Ǯ�� ������Ʈ �̸� ����
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);  // ������Ʈ ��Ȱ��ȭ
            pool.Enqueue(obj);  // ť�� �ֱ�
        }
    }


    // Ǯ���� ������Ʈ�� �������� �Լ�
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();  // ť���� ������Ʈ �ϳ� ������
            obj.SetActive(true);  // ������Ʈ Ȱ��ȭ
            return obj;
        }
        else
        {
            // �ʿ�� Ǯ�� ������Ʈ�� �������� �߰�
            GameObject obj = Instantiate(objectPrefab);
            return obj;
        }
    }

    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �Լ�
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  // ������Ʈ ��Ȱ��ȭ
        pool.Enqueue(obj);  // Ǯ�� ��ȯ
    }
}
