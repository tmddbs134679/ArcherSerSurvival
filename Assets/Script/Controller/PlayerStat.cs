using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat
{
    public float dodgeCoolTime = 3f;
    public float dodgePower = 5f;
    
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
                    animator.SetBool("IsDamaged", false);
                    isHpChanged = true;
                }
            }
        }
    }

    // 泥대젰媛먯냼 臾댁쟻?먯젙? collision?먯꽌 吏꾪뻾?좉쾬.
    public override void Damaged(float reduceHp)
    {
        if (isHpChanged)
        {
            base.Damaged(reduceHp);

            isHpChanged = false;
            timeHpDelay = 0;
            currentHp -= reduceHp;
            animator.SetBool("IsDamaged", true);

            if (currentHp <= 0)
            {
                Death();
            }   
        }
    }

    protected override void Death()
    {
        sRigidBody.velocity = Vector3.zero;

        // 二쎌쑝硫??щ챸?댁?湲?
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        // ?щ쭩?섎㈃ 紐⑤뱺 而댄룷?뚰듃 ?꾧린
        foreach (Behaviour componenet in transform.GetComponentsInChildren<Behaviour>())
        {
            componenet.enabled = false;
        }

        GameManager.Instance.GameOver();
    }
}
