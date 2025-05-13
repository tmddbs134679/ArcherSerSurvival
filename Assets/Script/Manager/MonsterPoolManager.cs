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
        objectPrefabs = Resources.LoadAll<GameObject>("Prefabs/Entity/Enemy");
        pool[0] = new Queue<GameObject>();
        pool[1] = new Queue<GameObject>();
        pool[2] = new Queue<GameObject>();

    }

    void Start()
    {
        // ???????듬땹??釉띾콦 亦껋꼶梨????諛댁뎽
        for (int j = 0; j < objectPrefabs.Length; j++)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectPrefabs[j]);
                int temp = j;
                obj.GetComponent<EnemyStat>().OnDie += () => ReturnObject(obj, temp);
                obj.SetActive(false);  // ???듬땹??釉띾콦 ?????繹먮봿??
                obj.transform.SetParent(gameObject.transform);
                pool[j].Enqueue(obj);  // ??????影?끸뵛
            }
        }
    }


    // ??????????듬땹??釉띾콦???띠럾??筌뤾쑴沅????貫??
    public GameObject GetObject(int index)
    {
        GameManager.Instance.EnemyCounting(1);
        if (pool[index].Count > 0)
        {
            GameObject obj = pool[index].Dequeue();  // ?????????듬땹??釉띾콦 ??濡る룎 ?怨쀫닑亦끸넂臾?
            obj.SetActive(true);  // ???듬땹??釉띾콦 ??戮?뎽??
            return obj;
        }
        else
        {
            // ?熬곣뫗??????????듬땹??釉띾콦?????됱쓤??怨쀬Ŧ ?怨뺣뼺?
            GameObject obj = Instantiate(objectPrefabs[index]);
            return obj;
        }
    }

    // ???????硫명뀊 ???듬땹??釉띾콦???????꾩룇瑗???濡ル츎 ??貫??
    public void ReturnObject(GameObject obj, int index)
    {
        GameManager.Instance.EnemyCounting(-1);
        obj.SetActive(false);  // ???듬땹??釉띾콦 ?????繹먮봿??
        pool[index].Enqueue(obj);  // ?????꾩룇瑗??
    }
}
