using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MonsterSpawnPoint : MonoBehaviour
{
    [Header("Spawn Point Settings")]
    [SerializeField] private float spawnAreaRadius = 3f;

    private Color spawnAreaColor = Color.red;

    public GameObject SpawnMonster(GameObject monsterPrefab)
    {
        // 스폰 영역 내 랜덤 위치에서 몬스터 생성
        Vector2 randomCirclePoint = Random.insideUnitCircle * spawnAreaRadius;
        Vector3 spawnPoint = new Vector3(
            transform.position.x + randomCirclePoint.x,
            transform.position.y + randomCirclePoint.y,
            transform.position.z);

        return Instantiate(monsterPrefab, spawnPoint, Quaternion.identity);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = new Color(spawnAreaColor.r, spawnAreaColor.g, spawnAreaColor.b, 0.2f); ;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, spawnAreaRadius);
    }
#endif
}

