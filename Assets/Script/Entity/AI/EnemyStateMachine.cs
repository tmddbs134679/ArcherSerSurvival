using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    public Dictionary<EENEMYSTATE, EnemyBaseState> States = new Dictionary<EENEMYSTATE, EnemyBaseState>();
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public List<SpriteRenderer> SpriteRenderers { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public EnemyAIController EnemyAIController { get; private set; }
    [field: SerializeField] public bool CanAttack { get; set; } = true;

    protected virtual void Awake()
    {
        States.Add(EENEMYSTATE.IDLE, new EnemyIdleState(this));
        States.Add(EENEMYSTATE.ATTACK, new EnemyAttackState(this));
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
}
