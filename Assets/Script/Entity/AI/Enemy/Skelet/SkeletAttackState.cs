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
        //??鴉????????ш낄援η뵳???ш끽維뽳쭛?熬곣뫀???
        timer += deltaTime;
        

        if(timer >= 0.2f)
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);


        //1?袁⑸즵獒뺣뎿???덉떵?Patroll ???ㅺ컼??
    }
    public override void Exit()
    {
        stateMachine.CanAttack = false;
    }
}
