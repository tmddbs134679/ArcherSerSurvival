using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        FlipX();


    }
    protected bool IsInChaseRange()
    {
        //if (stateMachine.Player.IsDead) { return false; }

    
        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }

    protected bool IsInAttackRange()
    {
        //if (stateMachine.Player.IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }

    protected void FlipX()
    {
        bool playerDir = false;

        playerDir = stateMachine.Player.transform.position.x < stateMachine.transform.position.x;

        foreach (SpriteRenderer spr in stateMachine.SpriteRenderers)
        {
            spr.flipX = playerDir;
        }
    }


}
