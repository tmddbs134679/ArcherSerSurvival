using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat
{
    public bool isInvincible = false;
    [SerializeField] float invincibleTime = 3f;

    [Header("Dodge")]
    [SerializeField] float dodgeDuration = 0.3f;

    private PlayerSFXControl sfxControl;
    public float DodgeDuration
    {
        get => dodgeDuration;
        set => dodgeDuration = Mathf.Clamp(value, 0, 5);
    }

    [SerializeField] float dodgeSpeed = 3f;
    public float DodgeSpeed
    {
        get => dodgeSpeed;
        set => dodgeSpeed = Mathf.Clamp(value, 0, 10);
    }
    
    [SerializeField] float dodgeCoolTime = 5f;
    public float DodgeCoolTime
    {
        get => dodgeCoolTime;
        set => dodgeCoolTime = Mathf.Clamp(value, 0, 10);
    }
    
    private Rigidbody2D sRigidBody;
    private Animator animator;

    private void Awake()
    {
        sRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sfxControl = GetComponent<PlayerSFXControl>();
    }

    // ?곕?吏 - 泥대젰 媛먯냼
    public override void Damaged(float reduceHp)
    {
        if (!isInvincible)
        {
            base.Damaged(reduceHp);
            sfxControl.OnHit();
            animator.SetTrigger("isDamaged");
           
            if (currentHp <= 0)
            {
                Death();
            }

            StartCoroutine(DamageDelayRoutine());
        }
    }

    private IEnumerator DamageDelayRoutine()
    {
        isInvincible = true;
        animator.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
        animator.SetLayerWeight(1, 0);
    }

    protected override void Death()
    {
        sRigidBody.velocity = Vector3.zero;

        // ?щ쭩 ???좊땲 ?ъ깮
        animator.SetLayerWeight(2, 1);

        GameManager.Instance.GameOver();
    }
}
