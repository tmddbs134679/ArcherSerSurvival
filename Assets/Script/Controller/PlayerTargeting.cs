using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    private Transform closestEnemy = null;
    [SerializeField] private Vector2 targetRange = new Vector2(5, 5);

    // 타겟 감지하여 타겟 위치를 return하는 함수
    public Transform GetClosestEnemy()
    {
        if (!PlayerController.Instance.isMoving)
        {
            // collider로 적을 감지 -> 최적화를 위해 배열 제한
            Collider2D[] enemiesInRange = new Collider2D[10];

            LayerMask enemyLayer = LayerMask.GetMask("Enemy");
            int count = Physics2D.OverlapBoxNonAlloc(transform.position, targetRange, 0f, enemiesInRange, enemyLayer);

            if (count == 0)
            {
                closestEnemy = null;
                return null;
            }

            float minDistance = Mathf.Infinity;

            for (int i = 0; i < count; i++)
            {
                float enemyDistance = Vector2.Distance(transform.position, enemiesInRange[i].transform.position);
                if (enemyDistance < minDistance)
                {
                    minDistance = enemyDistance;
                    closestEnemy = enemiesInRange[i].transform;
                }
            }

            if (closestEnemy != null)
            {
                float targetRangeDistance = targetRange.magnitude * 0.5f;
                float closestEnemyDistance = Vector2.Distance(transform.position, closestEnemy.position);

                if (closestEnemyDistance < targetRangeDistance)
                {
                    return closestEnemy;
                }
                else
                {
                    return null;
                }
            }
        }
        return null;
    }


    // 플레이어부터 타겟까지의 벡터를 return하는 함수
    public Vector2 EnemyDirection()
    {
        if (GetClosestEnemy() == null)
        {
            return Vector2.zero;
        }
        Transform target = GetClosestEnemy();
        return (target.position - transform.position).normalized;
    }


    // 타겟 감지 범위 gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, targetRange);
    }
}
