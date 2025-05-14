using System.Collections;
using UnityEngine;



public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform target;
    public float spawnHeight = 20f;

    void OnEnable()
    {
        ExplosionSkill.OnMeteorFired += DropMeteorDelayed;
    }

    void OnDisable()
    {
        ExplosionSkill.OnMeteorFired -= DropMeteorDelayed;
    }
    public void DropMeteorDelayed(GameObject target, float delay)
    {
        StartCoroutine(DropMeteorCoroutine(target, delay));
    }

    private IEnumerator DropMeteorCoroutine(GameObject target, float delay)
    {
        GameObject Zone = ProjectileObjectPool.Instance.Get("MeteoWarningZone");
        Zone.transform.position = target.transform.position;
        ProjectileObjectPool.Instance.ReleaseDelayed("MeteoWarningZone", Zone,4.6f);

        yield return new WaitForSeconds(delay * 1.2f);

        int posX = Random.Range(-10, 10);
        GameObject meteor = ProjectileObjectPool.Instance.Get("MeteoBundle");
        meteor.transform.position = target.transform.position + new Vector3(posX, spawnHeight, 0);
        Meteor meteorScript = meteor.GetComponent<Meteor>();
        meteorScript.targetPosition = target.transform.position;
    }
}
