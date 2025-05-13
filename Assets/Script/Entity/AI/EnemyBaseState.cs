using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
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
        Vector2 playerPos = stateMachine.Player.transform.position;
        Vector2 enemyPos = stateMachine.transform.position;

        float sqrDistance = ((Vector2)stateMachine.transform.position - (Vector2)stateMachine.Player.transform.position).sqrMagnitude;
        return sqrDistance <= stateMachine.MonsterData.attackRange * stateMachine.MonsterData.attackRange;
    }

    public void FlipX(Vector3 targetPos)
    {

        bool faceLeft = targetPos.x < stateMachine.transform.position.x;

        stateMachine.transform.rotation = Quaternion.Euler(0, faceLeft ? 180f : 0f, 0);
    }

    public void MoveToTarget(Vector2 target, float deltaTime)
    {
        FlipX(target);

        Vector2 current = stateMachine.transform.position;
        Vector2 dir = (target - current).normalized;
        stateMachine.transform.position += (Vector3)dir * stateMachine.MonsterData.movementSpeed * deltaTime;
    }


}
