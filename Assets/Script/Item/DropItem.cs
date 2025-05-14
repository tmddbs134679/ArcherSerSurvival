using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private const string goldDropName = "GoldDrop";
    private const string heartDropName = "RecoverHeart";
    private const string magnetDropName = "MagnetDrop";

    [SerializeField]
    [Range(0, 1)] private float goldDropPercentage;

    [SerializeField]
    [Range(0,1)] private float heartDropPercentage; 
    [SerializeField]
    [Range(0,1)] private float magnetDropPercentage;

    private EnemyStat enemy;
    private void Start()
    {
        enemy = GetComponent<EnemyStat>();
        if (enemy != null)
        {
            enemy.OnDie += Drop;
        }
    }

    public void Drop()
    {
        Debug.Log("item drop");
        if(Random.Range(0, 1f) < goldDropPercentage)
        {
            DropOnRandomPoint(goldDropName); 
        } 
        if(Random.Range(0, 1f) < heartDropPercentage)
        {
            DropOnRandomPoint(heartDropName); 
        }
        if(Random.Range(0, 1f) < magnetDropPercentage)
        {
            DropOnRandomPoint(magnetDropName); 
        }
    }

    private void DropOnRandomPoint(string name)
    {
        Vector2 spawnPoint = new Vector2(transform.position.x + Random.Range(0, 0.5f),
            transform.position.y + Random.Range(0, 0.5f));

        var item = ItemPool.Instance.GetObject(name);
        item.transform.position = spawnPoint;

        //Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
}
