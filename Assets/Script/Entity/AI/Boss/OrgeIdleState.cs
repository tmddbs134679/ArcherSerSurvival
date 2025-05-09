using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrgeIdleState : EnemyIdleState
{
    public OrgeIdleState(EnemyStateMachine stateMachine) : base(stateMachine)  { }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;

        Move(deltaTime);

        if (timer >= idleTime)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.SKILL]);
        }
    }

}
