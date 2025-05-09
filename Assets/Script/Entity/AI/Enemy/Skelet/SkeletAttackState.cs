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
        //���������� ���� �ӽ��ڵ�
        timer += deltaTime;
        

        if(timer >= 0.2f)
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);


        //1�߽�� Patroll ����
    }
    public override void Exit()
    {
      stateMachine.CanAttack = false;
    }
}
