using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileObjectPool : MonoBehaviour
{
    public static ProjectileObjectPool Instance { get; private set; }
    public GameObject[] projectilePrefabs; // ?щ윭 醫낅쪟???꾨━??

    private Dictionary<string, ObjectPool<GameObject>> pools = new Dictionary<string, ObjectPool<GameObject>>();

    private void Awake()
    {

        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
                projectilePrefabs = Resources.LoadAll<GameObject>("Prefabs/Skill/Data");
        foreach (var prefab in projectilePrefabs)
        {
            var pool = new ObjectPool<GameObject>(
                 () =>
                 {
                     var obj = Instantiate(prefab);
                     obj.transform.SetParent(this.transform); //?ㅻ툕?앺듃? 諛묒쑝濡??ъ궗泥??섏쐞?뚯씪 ?앹꽦
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

    // ??먯꽌 媛?몄삤湲?
    public GameObject Get(string prefabName)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            GameObject temp = pool.Get();
            return temp;

        }

        Debug.LogWarning($"???{prefabName}??媛) ?놁뒿?덈떎!");
        return null;
    }

    public void ReleaseDelayed(string prefabName, GameObject obj, float delay)
{
    StartCoroutine(ReleaseAfterDelay(prefabName, obj, delay));
}

private IEnumerator ReleaseAfterDelay(string prefabName, GameObject obj, float delay)
{
    yield return new WaitForSeconds(delay);
    Release(prefabName, obj);
}

    // ??먯꽌諛섑솚?섍린
    public void Release(string prefabName, GameObject obj)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            pool.Release(obj);
        }
        else
        {
            Destroy(obj); // ?놁쑝硫??뚭눼
        }
    }
}
