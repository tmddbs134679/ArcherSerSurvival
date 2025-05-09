using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyBaseState
{
    //Skils

    public EnemySkillState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Skill");
    }

 
    public override void Tick(float deltaTime)
    {
      
    }

    public override void Exit()
    {
      
    }

}
