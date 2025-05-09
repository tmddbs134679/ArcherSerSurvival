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


    public override void Tick(float deltaTime)
    {
        //빠저나가기 위한 임시코드
        timer += deltaTime;
        

        if(timer >= 0.2f)
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);


        //1발쏘면 Patroll 상태
    }
    public override void Exit()
    {
      stateMachine.CanAttack = false;
    }
}
