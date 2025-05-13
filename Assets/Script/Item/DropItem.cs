using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    GameObject goldDropPrefab;

    [SerializeField]
    GameObject heartDropPrefab;

    [SerializeField]
    [Range(0, 1)] private float goldDropPercentage;

    [SerializeField]
    [Range(0,1)] private float heartDropPercentage;

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
            DropOnRandomPoint(goldDropPrefab); 
        } 
        if(Random.Range(0, 1f) < heartDropPercentage)
        {
            DropOnRandomPoint(heartDropPrefab); 
        }
    }

    private void DropOnRandomPoint(GameObject prefab)
    {
        Vector2 spawnPoint = new Vector2(transform.position.x + Random.Range(0, 0.5f),
            transform.position.y + Random.Range(0, 0.5f));

        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
}
