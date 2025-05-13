using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletStateMachine : EnemyStateMachine
{



    protected override void Awake()
    {
        States.Add(EENEMYSTATE.IDLE, new SkeletIdleState(this));
        States.Add(EENEMYSTATE.PATROL, new EnemyPatrolState(this));
        States.Add(EENEMYSTATE.ATTACK, new SkeletAttackState(this));
        States.Add(EENEMYSTATE.CHASING, new EnemyChasingState(this));
    }

}
