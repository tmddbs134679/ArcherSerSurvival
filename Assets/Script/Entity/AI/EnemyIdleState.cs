using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    protected float idleTime;
    protected float timer;
    private readonly int IdleHas = Animator.StringToHash("Idle");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        timer = 0;
        idleTime = Random.Range(2, 3);
        Debug.Log("Idle");
        stateMachine.Animator.CrossFadeInFixedTime(IdleHas, CrossFadeDuration);
  
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;

        Move(deltaTime);

        if (IsInChaseRange())
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.CHASING]);
        }

        if (timer >= idleTime)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.PATROL]);
        }
    }


    public override void Exit()
    {
     
    }

}
