
using System.Collections;
using UnityEngine;

public class GoblinAttackState : EnemyAttackState
{
    private int maxCount = 5;
    private float rushDuration = 2f;

    public LayerMask wallLayer;

    private Coroutine billiardsCoroutine;
    private Vector2 startDir;

    public GoblinAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 플레이어 방향 기준 초기 방향 설정
        Vector2 dirToPlayer = (stateMachine.Player.transform.position - stateMachine.transform.position).normalized;
        startDir = dirToPlayer;

        billiardsCoroutine = stateMachine.StartCoroutine(Billiards(startDir));
    }
    public override void Tick(float deltaTime)
    {

    }


    public override void Exit()
    {
        base.Exit();

        if (billiardsCoroutine != null)
        {
            stateMachine.StopCoroutine(billiardsCoroutine);
        }
    }

    private IEnumerator Billiards(Vector2 direction)
    {
        int bounceCount = 0;
        Vector2 currentDir = direction;

        while (bounceCount < maxCount)
        {
            float step = stateMachine.MonsterData.movementSpeed * Time.deltaTime;

        
            stateMachine.transform.Translate(currentDir * step, Space.World);

        
            RaycastHit2D hit = Physics2D.Raycast(stateMachine.transform.position, currentDir, step + 0.1f, wallLayer);
            if (hit.collider != null)
            {
                currentDir = Vector2.Reflect(currentDir, hit.normal);
                bounceCount++;

            }

            yield return null;
        }

        stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);
    }

 
}

