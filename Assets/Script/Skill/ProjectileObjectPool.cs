using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileObjectPool : MonoBehaviour
{
    public static ProjectileObjectPool Instance { get; private set; }
    public List<GameObject> projectilePrefabs; // ?????ル굝履???袁ⓥ봺??

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
                     obj.transform.SetParent(this.transform); //??삵닏??븍뱜?? 獄쏅쵐?앮에???沅쀯㎗???륁맄???뵬 ??밴쉐
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

    // ???癒?퐣 揶쎛?紐꾩궎疫?
    public GameObject Get(string prefabName)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            GameObject temp = pool.Get();
            return temp;

        }

        Debug.LogWarning($"????{prefabName}??揶쎛) ??곷뮸??덈뼄!");
        return null;
    }

    // ??嚥?獄쏆꼹???띾┛
    public void Release(string prefabName, GameObject obj)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            pool.Release(obj);
        }
        else
        {
            Destroy(obj); // ??곸몵筌?域밸챶源????댘
        }
    }
}
