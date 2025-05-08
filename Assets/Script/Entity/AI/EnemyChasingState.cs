using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Chasing");
    }

    public override void Tick(float deltaTime)
    {
       
    }

    public override void Exit()
    {
    }

    private void MoveToPlayer(float deltaTime)
    {

        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            //Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }


        //stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }
}
