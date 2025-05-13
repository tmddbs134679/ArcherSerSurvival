using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform target;
    public float spawnHeight = 20f;

    void Start()
    {
        SpawnMeteor();
                SpawnMeteor();
                        SpawnMeteor();
                                SpawnMeteor();
                                        SpawnMeteor();
                                                SpawnMeteor();
                                                        SpawnMeteor();
    }
    public void SpawnMeteor()
    {
        target= PlayerController.Instance.gameObject.transform;
        int posX=Random.Range(-10, 10);
        Vector3 spawnPosition = new Vector3(target.position.x+posX, target.position.y + spawnHeight, target.position.z);
        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

        Meteor meteorScript = meteor.GetComponent<Meteor>();
        meteorScript.target = target;
    }
}
