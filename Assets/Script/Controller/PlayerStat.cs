using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat
{
    private Rigidbody2D sRigidBody;
    private Animator animator;
    
    private void Awake()
    {
        sRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        
        if (!isHpChanged)
        {
            if (timeHpDelay < hpChangeDelay)
            {
                timeHpDelay += Time.deltaTime;
                if (timeHpDelay >= hpChangeDelay)
                {
                    isHpChanged = true;
                }
            }
        }
    }

    // 체력감소 무적판정은 collision에서 진행할것.
    public override void  Damaged(float reduceHp)
    {
        if (isHpChanged)
        {
            base.Damaged(reduceHp);

            isHpChanged = false;
            timeHpDelay = 0;
            currentHp -= reduceHp;
            animator.SetTrigger("isDamaged");

            if (currentHp <= 0)
            {
                Death();
            }   
        }
    }

    protected override void Death()
    {
        sRigidBody.velocity = Vector3.zero;

        // 죽으면 투명해지기
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        // 사망하면 모든 컴포넌트 끄기
        foreach (Behaviour componenet in transform.GetComponentsInChildren<Behaviour>())
        {
            componenet.enabled = false;
        }

        // 사망 2초 후 제거
        Destroy(gameObject, 2f);
    }
}
