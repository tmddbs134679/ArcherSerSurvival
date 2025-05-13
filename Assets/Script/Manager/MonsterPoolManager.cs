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
        // ??????삵닏??븍뱜 沃섎챶????밴쉐
        for (int j = 0; j < objectPrefabs.Length; j++)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectPrefabs[j]);
                int temp = j;
                obj.GetComponent<EnemyStat>().OnDie += () => ReturnObject(obj, temp);
                obj.SetActive(false);  // ??삵닏??븍뱜 ??쑵??源딆넅
                obj.transform.SetParent(gameObject.transform);
                pool[j].Enqueue(obj);  // ?癒?퓠 ?節딅┛
            }
        }
    }


    // ???癒?퐣 ??삵닏??븍뱜??揶쎛?紐꾩궎????λ땾
    public GameObject GetObject(int index)
    {
        GameManager.Instance.EnemyCounting(1);
        if (pool[index].Count > 0)
        {
            GameObject obj = pool[index].Dequeue();  // ?癒?퓠????삵닏??븍뱜 ??롪돌 ?곗눖沅→묾?
            obj.SetActive(true);  // ??삵닏??븍뱜 ??뽮쉐??
            return obj;
        }
        else
        {
            // ?袁⑹뒄????????삵닏??븍뱜????덉읅??곗쨮 ?곕떽?
            GameObject obj = Instantiate(objectPrefabs[index]);
            return obj;
        }
    }

    // ???????멸텆 ??삵닏??븍뱜??????獄쏆꼹???롫뮉 ??λ땾
    public void ReturnObject(GameObject obj, int index)
    {
        GameManager.Instance.EnemyCounting(-1);
        obj.SetActive(false);  // ??삵닏??븍뱜 ??쑵??源딆넅
        pool[index].Enqueue(obj);  // ????獄쏆꼹??
    }
}
