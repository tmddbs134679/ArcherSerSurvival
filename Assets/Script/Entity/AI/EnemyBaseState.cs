using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        
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

    protected void FlipX(Vector3 targetPos)
    {
        bool faceLeft = targetPos.x < stateMachine.transform.position.x;

        foreach (SpriteRenderer spr in stateMachine.SpriteRenderers)
        {
            spr.flipX = faceLeft;
        }
    }

    public void MoveToTarget(Transform target, float deltaTime)
    {
        FlipX(target.position);
        Vector2 dir = (target.position - stateMachine.transform.position).normalized;
        stateMachine.transform.position += (Vector3)dir * stateMachine.MovementSpeed * deltaTime;
    }

}
