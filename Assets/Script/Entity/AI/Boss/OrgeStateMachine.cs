using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class OrgeStateMachine : EnemyStateMachine
{


    protected override void Awake()
    {
        States.Add(EENEMYSTATE.IDLE, new OrgeIdleState(this));
        States.Add(EENEMYSTATE.SKILL, new EnemySkillState(this));
        States.Add(EENEMYSTATE.CHASING, new OrgeChasingState(this));
    }

}
