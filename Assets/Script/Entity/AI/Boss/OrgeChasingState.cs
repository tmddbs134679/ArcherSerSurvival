using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrgeChasingState : EnemyChasingState
{
    private float chasingTime = 2f;
    private float timer;


    public OrgeChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = 0;
    }


    public override void Tick(float deltaTime)
    {
        
        timer += deltaTime;
        MoveToTarget((Vector2)stateMachine.Player.transform.position, deltaTime);

        if (timer > chasingTime)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);      
        }

    }
}
