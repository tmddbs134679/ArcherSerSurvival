
using System.Collections;
using System.Threading;
using UnityEngine;

public class GoblinAttackState : EnemyAttackState
{
    public LayerMask wallLayer;


    public GoblinAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        var monster = stateMachine.GetComponent<Goblin>();
        monster.Attack(OnChangeIdle);
    }
    public override void Tick(float deltaTime)
    {

    }


    public override void Exit()
    {

        
    }

    void OnChangeIdle()
    {
        stateMachine.SwitchState(stateMachine.States[EENEMYSTATE.IDLE]);
    }
   
 
}

