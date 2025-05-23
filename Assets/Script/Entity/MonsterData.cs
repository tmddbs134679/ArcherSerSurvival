using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Enemy/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string monsterId;
    public EENEMYTYPE monsterType;
    public int attackDamage;
    public float attackRange;
    public float movementSpeed;
    public float PlayerChasingRange;
}