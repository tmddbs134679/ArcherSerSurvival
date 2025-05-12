using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileObjectPool : MonoBehaviour
{
    public static ProjectileObjectPool Instance { get; private set; }
    public List<GameObject> projectilePrefabs; // ?�러 종류???�리??

    private Dictionary<string, ObjectPool<GameObject>> pools = new Dictionary<string, ObjectPool<GameObject>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var prefab in projectilePrefabs)
        {
            var pool = new ObjectPool<GameObject>(
                 () =>
                 {
                     var obj = Instantiate(prefab);
                     obj.transform.SetParent(this.transform); //?�브?�트?� 밑으�??�사�??�위?�일 ?�성
                     return obj;
                 },
                 obj => obj.SetActive(true),
                 obj => obj.SetActive(false),
                 obj => Destroy(obj),
                 false,10, 100
             );
            pools.Add(prefab.name, pool);
        }
    }

    // ?�?�서 가?�오�?
    public GameObject Get(string prefabName)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            GameObject temp = pool.Get();
            return temp;

        }

        Debug.LogWarning($"?�??{prefabName}??가) ?�습?�다!");
        return null;
    }

    // ?��?반환?�기
    public void Release(string prefabName, GameObject obj)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            pool.Release(obj);
        }
        else
        {
            Destroy(obj); // ?�으�?그냥 ?�괴
        }
    }
}
