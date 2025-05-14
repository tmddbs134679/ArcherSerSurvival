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
        //??辱??????????꾣뤃管逾?????썹땟戮녹춿??ш끽維???
        timer += deltaTime;
        

        if(timer >= 0.2f)
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);


        //1?熬곣뫖利든뜏類ｋ렲????됰뼲?Patroll ????븐뻤??
    }
    public override void Exit()
    {
        stateMachine.CanAttack = false;
    }
}
