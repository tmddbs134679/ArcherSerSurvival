using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrgeIdleState : EnemyIdleState
{
    public OrgeIdleState(EnemyStateMachine stateMachine) : base(stateMachine)  { }

    public override void Tick(float deltaTime)
    {

        Move(deltaTime);

        timer += deltaTime;

        if (timer >= idleTime)
        {
            float chance = Random.value;

            if (chance < 0.33f)
            {
                stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.CHASING]);
            }
            else
            {
                stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.SKILL]);
            }
        }
    }

}
