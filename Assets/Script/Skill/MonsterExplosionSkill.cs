using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterExplosionSkill : ExplosionSkill
{
    [SerializeField] private int fireCount = 5;


    protected override void Update()
    {
       
    }
    protected override void Init()
    {
        base.Init();
        animationName = Animator.StringToHash("Skill3");
    }

    public override void Execute(EnemyStateMachine enemy, Action onComplete)
    {
        GameObject target = GetComponent<OrgeStateMachine>().Player;
        Fire(fireCount, this.gameObject, target); // 硫뷀뀒???섍린
        StartCoroutine(DelayComplete(onComplete));
    }

    private IEnumerator DelayComplete(Action onComplete)
    {
        yield return new WaitForSeconds(3f);
        onComplete?.Invoke();
    }
}
