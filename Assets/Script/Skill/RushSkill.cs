using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RUSHDIR
{
    HORIZONTAL,
    VERTICAL,
    ALL
}

public class RushSkill : MonoBehaviour
{
    [Header("Map-based Rush Positions")]
    [SerializeField] private Vector2 leftEdgePos;
    [SerializeField] private Vector2 rightEdgePos;
    [SerializeField] private Vector2 topEdgePos;
    [SerializeField] private Vector2 bottomEdgePos;

    //임시 스킬 Name
    public int animationName = Animator.StringToHash("Skill2");

    public RUSHDIR directionType = RUSHDIR.ALL;
    public float rushDistance = 5f;
    public float rushDuration = 0.4f;

    public int minRushCount = 1;
    public int maxRushCount = 3;

    public void Execute(EnemyStateMachine enemy, Action onComplete)
    {
        enemy.StartCoroutine(RushSequence(enemy, onComplete));
    }

    private IEnumerator RushSequence(EnemyStateMachine enemy, Action onComplete)
    {
        int rushCount = Random.Range(minRushCount, maxRushCount + 1);

        for (int i = 0; i < rushCount; i++)
        {
            RUSHDIR dirType = GetRandomDirectionType();

            Vector2 start = Vector2.zero;
            Vector2 end = Vector2.zero;

            Vector2 playerPos = enemy.Player.transform.position;

            switch (dirType)
            {
                case RUSHDIR.HORIZONTAL:
                    if (Random.value > 0.5f)
                    {
                        start = new Vector2(leftEdgePos.x, playerPos.y);  
                        end = new Vector2(rightEdgePos.x, playerPos.y);
                    }
                    else
                    {
                        start = new Vector2(rightEdgePos.x, playerPos.y);
                        end = new Vector2(leftEdgePos.x, playerPos.y);
                    }
                    break;

                case RUSHDIR.VERTICAL:
                    if (Random.value > 0.5f)
                    {
                        start = new Vector2(playerPos.x, topEdgePos.y);    
                        end = new Vector2(playerPos.x, bottomEdgePos.y);
                    }
                    else
                    {
                        start = new Vector2(playerPos.x, bottomEdgePos.y);
                        end = new Vector2(playerPos.x, topEdgePos.y);
                    }
                    break;
            }
            yield return Rush(enemy, start, end);
        }

        onComplete?.Invoke();
    }

    private RUSHDIR GetRandomDirectionType()
    {
        switch (directionType)
        {
            case RUSHDIR.HORIZONTAL:
            case RUSHDIR.VERTICAL:
                return directionType;
            case RUSHDIR.ALL:
                return Random.value > 0.5f ? RUSHDIR.HORIZONTAL : RUSHDIR.VERTICAL;
            default:
                return RUSHDIR.HORIZONTAL;
        }
    }
    private IEnumerator Rush(StateMachine enemy, Vector2 start, Vector2 end)
    {
        float elapsed = 0f;

        while (elapsed < rushDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / rushDuration);
            enemy.transform.position = Vector2.Lerp(start, end, t);
            yield return null;
        }

        enemy.transform.position = end;
    }
}
