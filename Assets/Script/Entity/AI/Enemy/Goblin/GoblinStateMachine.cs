using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStateMachine : EnemyStateMachine
{
    protected override void Awake()
    {
        States.Add(EENEMYSTATE.IDLE, new GoblinIdleState(this));
        States.Add(EENEMYSTATE.ATTACK, new GoblinAttackState(this));
        States.Add(EENEMYSTATE.Dead, new EnemyDeadState(this));
    }
}
