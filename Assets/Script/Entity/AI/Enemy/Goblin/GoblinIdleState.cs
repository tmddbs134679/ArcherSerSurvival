using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinIdleState : EnemyIdleState
{
    
    public GoblinIdleState(EnemyStateMachine stateMachine) : base(stateMachine){ }
    public override void Enter()
    {
        base.Enter();
    }


    public override void Tick(float deltaTime)
    {
        timer += deltaTime;

        Move(deltaTime);

        if (timer >= idleTime)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.ATTACK]);
        }
    }


}
