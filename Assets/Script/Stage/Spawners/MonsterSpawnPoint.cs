using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterSpawnPoint : MonoBehaviour
{
    [Header("Spawn Point Settings")]
    [SerializeField] private float spawnAreaRadius = 3f;

    private Color spawnAreaColor = Color.red;

    public GameObject SpawnMonster(GameObject monsterPrefab)
    {
        // ���� ���� �� ���� ��ġ���� ���� ����
        Vector2 randomCirclePoint = Random.insideUnitCircle * spawnAreaRadius;
        Vector3 spawnPoint = new Vector3(
            transform.position.x + randomCirclePoint.x,
            transform.position.y + randomCirclePoint.y,
            transform.position.z);

        return Instantiate(monsterPrefab, spawnPoint, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Handles.color = new Color(spawnAreaColor.r, spawnAreaColor.g, spawnAreaColor.b, 0.2f); ;
        Handles.DrawSolidDisc(transform.position, Vector3.forward, spawnAreaRadius);
    }
}
