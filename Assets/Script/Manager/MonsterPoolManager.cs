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
        // ????ㅻ툕?앺듃 誘몃━ ?앹꽦
        for (int j = 0; j < objectPrefabs.Length; j++)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectPrefabs[j]);
                int temp = j;
                obj.GetComponent<EnemyStat>().OnDie += () => ReturnObject(obj, temp);
                obj.SetActive(false);  // ?ㅻ툕?앺듃 鍮꾪솢?깊솕
                obj.transform.SetParent(gameObject.transform);
                pool[j].Enqueue(obj);  // ?먯뿉 ?ｊ린
            }
        }
    }


    // ??먯꽌 ?ㅻ툕?앺듃瑜?媛?몄삤???⑥닔
    public GameObject GetObject(int index)
    {
        GameManager.Instance.EnemyCounting(1);
        if (pool[index].Count > 0)
        {
            GameObject obj = pool[index].Dequeue();  // ?먯뿉???ㅻ툕?앺듃 ?섎굹 爰쇰궡湲?
            obj.SetActive(true);  // ?ㅻ툕?앺듃 ?쒖꽦??
            return obj;
        }
        else
        {
            // ?꾩슂??????ㅻ툕?앺듃瑜??숈쟻?쇰줈 異붽?
            GameObject obj = Instantiate(objectPrefabs[index]);
            return obj;
        }
    }

    // ?ъ슜???앸궃 ?ㅻ툕?앺듃瑜????諛섑솚?섎뒗 ?⑥닔
    public void ReturnObject(GameObject obj, int index)
    {
        GameManager.Instance.EnemyCounting(-1);
        obj.SetActive(false);  // ?ㅻ툕?앺듃 鍮꾪솢?깊솕
        pool[index].Enqueue(obj);  // ???諛섑솚
    }
}
