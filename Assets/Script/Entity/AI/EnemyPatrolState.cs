using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private int currentPointIndex = 0;
    private float reach = 0.1f;
    private List<Vector2> patrolPoints => stateMachine.EnemyAIController.PatrolPositions;

    private Vector2? currentDestination = null;

    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Patrol");

        if (patrolPoints == null || patrolPoints.Count == 0)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);
            return;
        }

        SetNextDestination();
    }

    public override void Tick(float deltaTime)
    {
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.CHASING]);
            return;
        }

        if (currentDestination.HasValue)
        {
            Vector2 pos = stateMachine.transform.position;
            Vector2 dest = currentDestination.Value;

            MoveToTarget(dest, deltaTime);

            if (Vector2.Distance(pos, dest) <= reach)
            {
                currentDestination = null;
                stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);
            }
        }
    }

    public override void Exit()
    {
        stateMachine.CanAttack = true;
    }

    private void SetNextDestination()
    {
        Vector2 current = stateMachine.transform.position;
        Vector2 target = patrolPoints[currentPointIndex];

        RaycastHit2D hit = Physics2D.Linecast(current, target, stateMachine.wallLayer);

        if (hit.collider != null)
        {
            currentDestination = hit.point - (target - current).normalized * 0.1f; 
        }
        else
        {
            currentDestination = target;
        }

    
        currentPointIndex = Random.Range(0, patrolPoints.Count);
    }
}
