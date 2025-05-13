using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    public Dictionary<EENEMYSTATE, EnemyBaseState> States = new Dictionary<EENEMYSTATE, EnemyBaseState>();
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public List<SpriteRenderer> SpriteRenderers { get; private set; }
    [field: SerializeField] public MonsterData MonsterData { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public EnemyStat EnemyStat { get; private set; }

    [field: SerializeField] public List<RushSkill> Skills { get; private set; }
    [field: SerializeField] public EnemyAIController EnemyAIController { get; private set; }
    [field: SerializeField] public bool CanAttack { get; set; } = true;

    [field: SerializeField] public bool CanChasing { get; set; } = true;

    [field: SerializeField] public bool CanStun { get; set; } = true;
    private void OnEnable()
    {
        EnemyStat.OnTakeDamage += HandleTakeDamage;
        EnemyStat.OnDie += HandleDie;
    }

 

    private void OnDisable() 
    {
        EnemyStat.OnTakeDamage -= HandleTakeDamage;
        EnemyStat.OnDie -= HandleDie;
    }

    [SerializeField] public LayerMask wallLayer;
    protected virtual void Awake()
    {
        States.Add(EENEMYSTATE.IDLE, new EnemyIdleState(this));
        States.Add(EENEMYSTATE.ATTACK, new EnemyAttackState(this));
        States.Add(EENEMYSTATE.DEAD, new EnemyDeadState(this));
        States.Add(EENEMYSTATE.STUN, new EnemyStunState(this));
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        SwitchState(States[EENEMYSTATE.IDLE]);
    }

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    private void HandleDie()
    {
        SwitchState(States[EENEMYSTATE.DEAD]);
    }
    private void HandleTakeDamage()
    {
           if(CanStun)
            SwitchState(States[EENEMYSTATE.STUN]);
           else
           {
               Animator.SetLayerWeight(1, 1);
           }
        


    }

}
