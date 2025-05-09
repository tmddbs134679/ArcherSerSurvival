using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private int currentPointIndex = 0;
    private float reach = 0.1f;
    private List<Transform> patrolPoints => stateMachine.EnemyAIController.patrolPoints;

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

        Transform target = patrolPoints[currentPointIndex];

        MoveToTarget(target, deltaTime);

        if(Vector2.Distance(stateMachine.transform.position, target.position) < reach)
        {
            currentPointIndex = Random.Range(0, patrolPoints.Count);
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]); 
        }
    }
 
}
