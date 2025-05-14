using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySkillState : EnemyBaseState
{
    private int SkillHas;
    private const float CrossFadeDuration = 0.1f;

    private bool isSkillFinished = false;

    public EnemySkillState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //가중치 가지고와서 idx로 변환
        string weight = stateMachine.SkillWeight.GetRandom();
        int idx = int.Parse(weight);

        SkillHas = stateMachine.Skills[idx].animationName;

        stateMachine.Animator.CrossFadeInFixedTime(SkillHas, CrossFadeDuration);

        // 콜백 전달: 스킬이 끝나면 OnSkillComplete 호출
        stateMachine.Skills[idx].Execute(stateMachine, OnSkillComplete);
    }

    public override void Tick(float deltaTime)
    {
        if (isSkillFinished)
        {
            stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);
        }
    }

    public override void Exit()
    {
        isSkillFinished = false; // 혹시 상태 재사용 시 안전하게 초기화
    }

    private void OnSkillComplete()
    {
        isSkillFinished = true;
    }
}
