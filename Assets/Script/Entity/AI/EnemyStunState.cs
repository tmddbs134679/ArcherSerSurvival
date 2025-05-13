using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : EnemyBaseState
{
    private readonly int StunHas = Animator.StringToHash("Stun");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    private float duration = 0.3f;

    public EnemyStunState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Stun");
        duration = 0.3f;
        stateMachine.Animator.CrossFadeInFixedTime(StunHas, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;

        if (duration <= 0f)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);
        }
    }
    public override void Exit()
    {
        
    }

}
