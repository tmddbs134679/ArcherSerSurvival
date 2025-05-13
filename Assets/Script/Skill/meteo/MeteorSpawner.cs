using System.Collections;
using UnityEngine;



public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform target;
    public float spawnHeight = 20f;

void OnEnable()
{
    ExplosionSkill.OnExplosionSkillFired += DropMeteorDelayed;
}

void OnDisable()
{
    ExplosionSkill.OnExplosionSkillFired -= DropMeteorDelayed;
}
public void DropMeteorDelayed(GameObject target, float delay)
{
    StartCoroutine(DropMeteorCoroutine(target, delay));
}

private IEnumerator DropMeteorCoroutine(GameObject target, float delay)
{
    yield return new WaitForSeconds(delay*1.2f);
                    int posX = Random.Range(-10, 10);
GameObject meteor =ProjectileObjectPool.Instance.Get("MeteoBundle");
meteor.transform.position=target.transform.position+new Vector3(posX,spawnHeight,0);
    Meteor meteorScript = meteor.GetComponent<Meteor>();
meteorScript.targetPosition = target.transform.position;
}
}
