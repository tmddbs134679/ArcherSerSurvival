using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MonsterSpawnPoint : MonoBehaviour
{
    [Header("Spawn Point Settings")]
    [SerializeField] private float spawnAreaRadius = 3f;

    private Color spawnAreaColor = Color.red;

    public Vector2 GetRandomPoint()
    {
        // ���� ���� �� ���� ��ġ���� ���� ����
        Vector2 randomCirclePoint = Random.insideUnitCircle * spawnAreaRadius;
        Vector2 spawnPoint = new Vector2(
            transform.position.x + randomCirclePoint.x,
            transform.position.y + randomCirclePoint.y);

        return spawnPoint;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = new Color(spawnAreaColor.r, spawnAreaColor.g, spawnAreaColor.b, 0.2f); ;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, spawnAreaRadius);
    }
#endif
}

