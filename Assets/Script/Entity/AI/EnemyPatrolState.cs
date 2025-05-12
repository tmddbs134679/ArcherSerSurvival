using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private int currentPointIndex = 0;
    private float reach = 0.1f;
    private List<Vector2> patrolPoints => stateMachine.EnemyAIController.PatrolPositions;

    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
      
    }

    public override void Enter()
    {
        Debug.Log("Patrol");
    }

    public override void Tick(float deltaTime)
    {
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.CHASING]);
            return;
        }

        MoveToNextPatrolPoint(deltaTime);

    }

    public override void Exit()
    {
        stateMachine.CanAttack = true;
    }

    private void MoveToNextPatrolPoint(float deltaTime)
    {
        if (patrolPoints == null || patrolPoints.Count == 0)
            return;

        Vector2 target = patrolPoints[currentPointIndex];

        Vector2 current = stateMachine.transform.position;
        Vector2 destination = target;

        RaycastHit2D hit = Physics2D.Linecast(current, destination, stateMachine.wallLayer);

        if (hit.collider != null)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);
            return;
        }




        MoveToTarget(target, deltaTime);

        if(Vector2.Distance(stateMachine.transform.position, target) < reach)
        {
            currentPointIndex = Random.Range(0, patrolPoints.Count);
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]); 
        }
    }
 
}
