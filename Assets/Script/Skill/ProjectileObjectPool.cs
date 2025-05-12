using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileObjectPool : MonoBehaviour
{
    public static ProjectileObjectPool Instance { get; private set; }
    public List<GameObject> projectilePrefabs; // ?¨Îü¨ Ï¢ÖÎ•ò???ÑÎ¶¨??

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
                     obj.transform.SetParent(this.transform); //?§Î∏å?ùÌä∏?Ä Î∞ëÏúºÎ°??¨ÏÇ¨Ï≤??òÏúÑ?åÏùº ?ùÏÑ±
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

    // ?Ä?êÏÑú Í∞Ä?∏Ïò§Í∏?
    public GameObject Get(string prefabName)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            GameObject temp = pool.Get();
            return temp;

        }

        Debug.LogWarning($"?Ä??{prefabName}??Í∞Ä) ?ÜÏäµ?àÎã§!");
        return null;
    }

    // ?ÄÎ°?Î∞òÌôò?òÍ∏∞
    public void Release(string prefabName, GameObject obj)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            pool.Release(obj);
        }
        else
        {
            Destroy(obj); // ?ÜÏúºÎ©?Í∑∏ÎÉ• ?åÍ¥¥
        }
    }
}
