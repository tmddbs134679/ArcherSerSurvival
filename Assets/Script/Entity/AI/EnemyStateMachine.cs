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

    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public EnemyAIController EnemyAIController { get; private set; }
    [field: SerializeField] public bool CanAttack { get; set; } = true;

    private void OnEnable()
    {
       // Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void HandleTakeDamage()
    {
        
    }

    private void OnDisable()
    {
        //Health.OnTakeDamage -= HandleTakeDamage;
         Health.OnDie -= HandleDie;
    }

    [SerializeField] public LayerMask wallLayer;
    protected virtual void Awake()
    {
        States.Add(EENEMYSTATE.IDLE, new EnemyIdleState(this));
        States.Add(EENEMYSTATE.ATTACK, new EnemyAttackState(this));
        States.Add(EENEMYSTATE.Dead, new EnemyDeadState(this));
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
        SwitchState(new EnemyDeadState(this));
    }


}
