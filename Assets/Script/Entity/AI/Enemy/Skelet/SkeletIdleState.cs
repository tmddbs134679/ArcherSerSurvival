using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletIdleState : EnemyIdleState
{
    public SkeletIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Tick(float deltaTime)
    {
        timer += deltaTime;

        Move(deltaTime);

        //TODO : AttackRange에 들어와서 공격하고 다음 Patorl로 도착할떄까지 공격 못하게 (아마)
        if (IsInAttackRange() && stateMachine.CanAttack)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.ATTACK]);
        }

        if (timer >= idleTime)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.PATROL]);
        }
    }

}
