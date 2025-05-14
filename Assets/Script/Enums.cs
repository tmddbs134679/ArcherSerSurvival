using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EENEMYSTATE
{
    IDLE,
    RUN,
    MOVE,
    PATROL,
    ATTACK,
    SKILL,
    STUN,
    CHASING,
    DEAD,
}

public enum EPATROLAXIS
{
    HORIZONTAL,
    VERTICAL,
    ALL
}

public enum EENEMYTYPE
{
    DOC,
    SKELET,
    OGRE,
    GOBLIN,
}