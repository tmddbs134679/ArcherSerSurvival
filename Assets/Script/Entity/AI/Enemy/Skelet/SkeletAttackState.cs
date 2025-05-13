using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletAttackState : EnemyAttackState
{
    private float timer;
    private float testTime = 0.2f;
    public SkeletAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.GetComponent<MonsterProjectileSkill>().Fire(1, stateMachine.gameObject, stateMachine.Player);

    }
    public override void Tick(float deltaTime)
    {
        //??쥙????疫??袁る립 ?袁⑸뻻?꾨뗀諭?
        timer += deltaTime;
        

        if(timer >= 0.2f)
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);


        //1獄쏆뮇猷쒙쭖?Patroll ?怨밴묶
    }
    public override void Exit()
    {
        stateMachine.CanAttack = false;
    }
}
