using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


[System.Serializable]
public class PoolEntry
{
    public string key;
    public GameObject prefab;

}
public class ProjectileObjectPool : Singleton<ProjectileObjectPool>
{
   // public static ProjectileObjectPool Instance { get; private set; }
    public GameObject[] projectilePrefabs; // ?????ル굝履???袁ⓥ봺??

    private Dictionary<string, ObjectPool<GameObject>> pools = new Dictionary<string, ObjectPool<GameObject>>();

    protected override void Awake()
    {
        base.Awake();

                projectilePrefabs = Resources.LoadAll<GameObject>("Prefabs/Skill/Data");
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

    public void ReleaseDelayed(string prefabName, GameObject obj, float delay)
{
    StartCoroutine(ReleaseAfterDelay(prefabName, obj, delay));
}

private IEnumerator ReleaseAfterDelay(string prefabName, GameObject obj, float delay)
{
    yield return new WaitForSeconds(delay);
    Release(prefabName, obj);
}

    // ???癒?퐣獄쏆꼹???띾┛
    public void Release(string prefabName, GameObject obj)
    {
        if (pools.TryGetValue(prefabName, out var pool))
        {
            pool.Release(obj);
        }
        else
        {
            Destroy(obj); // ??곸몵筌????댘
        }
    }

    public void AllObjectOff()
    {
        //???뗣뀑 諛뽰쓣 媛?몄??쇱? ?뗣뀑



        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
            pools[child.gameObject.name.Substring(0, child.gameObject.name.Length - "(Clone)".Length)].Release(child.gameObject);
        }

    }
}
