using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    [Header("Patrol Settings")]
    public EPATROLAXIS patrolAxis = EPATROLAXIS.ALL;
    public List<Transform> patrolPoints = new List<Transform>();
    public float patrolDistance = 3f;

    private void OnEnable()
    {
        CreatePatrolPoints();
    }

    private void CreatePatrolPoints()
    {
        patrolPoints.Clear();
        Vector2 standardPos = transform.position;
        List<Vector2> dirs = new();

        switch (patrolAxis)
        {
            case EPATROLAXIS.HORIZONTAL:
                dirs.Add(Vector2.left);
                dirs.Add(Vector2.right);
                break;

            case EPATROLAXIS.VERTICAL:
                dirs.Add(Vector2.up);
                dirs.Add(Vector2.down);
                break;

            case EPATROLAXIS.ALL:
                dirs.Add(Vector2.up);
                dirs.Add(Vector2.down);
                dirs.Add(Vector2.left);
                dirs.Add(Vector2.right);
                break;
        }

        foreach (var dir in dirs)
        {
            GameObject point = new GameObject("PatrolPoint");
            point.transform.position = standardPos + dir * patrolDistance;
            patrolPoints.Add(point.transform);
        }
    }
}
