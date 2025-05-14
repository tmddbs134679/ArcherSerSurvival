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
        RandomCheck = !RandomCheck;
        GameObject target = GetComponent<OrgeStateMachine>().Player;
        StartCoroutine(FireWithDelay(fireCount));
        StartCoroutine(DelayComplete(onComplete));
    }

    private IEnumerator DelayComplete(Action onComplete)
    {
        yield return new WaitForSeconds(3f);
        onComplete?.Invoke();
    }

    public IEnumerator FireWithDelay(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject TargetTemp = null;

            if (SkillOwner.layer == LayerMask.NameToLayer("Player")) //SkillOwner가 플레이어일시 타겟 탐색
            {
                TargetTemp = SkillOwner.GetComponent<PlayerTargeting>().GetClosestEnemy()?.gameObject;
            }
            else
            {
                TargetTemp = GetComponent<EnemyStateMachine>().Player;//아닐시 몬스터
            }
            if (TargetTemp == null) yield break;
            Fire(i, SkillOwner, TargetTemp);
            yield return new WaitForSeconds(individualFireRate);

        }
    }

}
