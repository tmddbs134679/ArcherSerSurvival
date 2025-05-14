using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    private readonly int DeadHas = Animator.StringToHash("Dead");
    private const float CrossFadeDuration = 0.1f;
    
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.Animator.CrossFadeInFixedTime(DeadHas, CrossFadeDuration);
    }

    public override void Enter()
    {
        stateMachine.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

 

    public override void Tick(float deltaTime)
    {
        AnimatorStateInfo stateInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 1f)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]); 
        }

    }

    public override void Exit()
    {
        MonsterPoolManager.Instance.ReturnObject(stateMachine.gameObject, stateMachine.MonsterData.monsterId);
    }
}
