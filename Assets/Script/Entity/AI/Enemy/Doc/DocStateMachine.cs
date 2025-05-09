using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocStateMachine : EnemyStateMachine
{

    protected override void Awake()
    {
        base.Awake();

        States.Add(EENEMYSTATE.PATROL, new EnemyPatrolState(this));
        States.Add(EENEMYSTATE.CHASING, new EnemyChasingState(this));
    }
    // Start is called before the first frame update



}
