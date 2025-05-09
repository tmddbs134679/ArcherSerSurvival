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

        //TODO : AttackRange�� ���ͼ� �����ϰ� ���� Patorl�� �����ҋ����� ���� ���ϰ� (�Ƹ�)
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
