using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : Singleton<ItemPool>
{
    public GameObject[] itemPrefabs;
    public Dictionary<string, GameObject> itemPrefabsDic = new Dictionary<string, GameObject>();

    [SerializeField]
    private int poolSize = 10;

    private Dictionary<string, Queue<GameObject>> itemPool = new Dictionary<string, Queue<GameObject>>();

    protected override void Awake()
    {
        base.Awake();
        itemPrefabs = Resources.LoadAll<GameObject>("Prefabs/Interactable/Item");

        foreach(var item in itemPrefabs)
        {
            Debug.Log(item.name);
            itemPrefabsDic.Add(item.name, item);
            itemPool[item.name] = new Queue<GameObject>();
        }
    }

    private void Start()
    {
        foreach (var key in itemPrefabsDic.Keys)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(itemPrefabsDic[key]);
                obj.SetActive(false); 
                obj.transform.SetParent(gameObject.transform);
                itemPool[key].Enqueue(obj);  
            }
        }
    }

    public GameObject GetObject(string key)
    {
        if (itemPool[key].Count > 0)
        {
            GameObject obj = itemPool[key].Dequeue(); 
            obj.SetActive(true);  
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(itemPrefabsDic[key]);
            obj.transform.SetParent(gameObject.transform);
            return obj;
        }
    }

    public GameObject[] GetActiveGoldItems()
    {
        GameObject[] goldItems = new GameObject[itemPool["GoldDrop"].Count];
        int index = 0;
        foreach(GameObject item in itemPool["GoldDrop"])
        {
            if (item.activeSelf)
            {
                goldItems[index++] = item;
            }
        }

        return goldItems;
    }

    public void ReturnObject(GameObject obj, string key)
    {
        obj.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        obj.SetActive(false);  
        itemPool[key].Enqueue(obj);  
    }

    public void AllObjectOff()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
            itemPool[child.gameObject.name.Substring(0, child.gameObject.name.Length - "(Clone)".Length)].Enqueue(child.gameObject);
        }

    }
}

