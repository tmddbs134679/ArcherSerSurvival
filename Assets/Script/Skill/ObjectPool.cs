using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileObjectPool : MonoBehaviour
{
    public List<GameObject> projectilePrefabs; // 여러 종류의 프리팹

    private Dictionary<string, ObjectPool<GameObject>> pools = new Dictionary<string, ObjectPool<GameObject>>();

    private void Awake()
    {
        foreach (var prefab in projectilePrefabs)
        {
       var pool = new ObjectPool<GameObject>(
            () => {
                var obj = Instantiate(prefab);
                obj.transform.SetParent(this.transform);
                return obj;
            },
            obj => obj.SetActive(true),
            obj => obj.SetActive(false),
            obj => Destroy(obj),
            false, 10, 100
        );
            pools.Add(prefab.name, pool);
        }
    }

    // 풀에서 가져오기
    public GameObject Get(string prefabName)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            return pool.Get();
        }

        Debug.LogWarning($"Pool for {prefabName} not found!");
        return null;
    }

    // 풀로 반환하기
    public void Release(string prefabName, GameObject obj)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            pool.Release(obj);
        }
        else
        {
            Destroy(obj); // 없으면 그냥 파괴
        }
    }
}
