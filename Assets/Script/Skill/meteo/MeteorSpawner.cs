using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform target;
    public float spawnHeight = 20f;

    void Start()
    {
        SpawnMeteor();
    }
    public void SpawnMeteor()
    {
        Vector3 spawnPosition = new Vector3(target.position.x, target.position.y + spawnHeight, target.position.z);
        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

        Meteor meteorScript = meteor.GetComponent<Meteor>();
        meteorScript.target = target;
    }
}
