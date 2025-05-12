using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int RunHas = Animator.StringToHash("Run");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Chasing");
        stateMachine.Animator.CrossFadeInFixedTime(RunHas, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
       
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);
        }

        if(IsInAttackRange())
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.ATTACK]);
        }

        MoveToTarget((Vector2)stateMachine.Player.transform.position, deltaTime);
    }

    public override void Exit()
    {
    }


}
