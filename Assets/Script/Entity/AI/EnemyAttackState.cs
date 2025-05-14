using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private readonly int AttackHas = Animator.StringToHash("Attack");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine){ }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AttackHas, CrossFadeDuration);

        //Debug.Log("Attack");
    }

    public override void Tick(float deltaTime)
    {
        //Move(deltaTime);

        if (!IsInAttackRange())
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.CHASING]);
        }
    }

    public override void Exit()
    {

    }
}
