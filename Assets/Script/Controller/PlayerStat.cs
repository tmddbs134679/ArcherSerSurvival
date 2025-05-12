using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private Rigidbody2D sRigidBody;
    private Animator animator;
    
    [SerializeField] private float maxHp = 10f;
    public float MaxHp
    {
        get => maxHp;
        set => maxHp = Mathf.Clamp(value, 0, 100);
    }
    float currentHp;

    float timeHpDelay = 0f;
    [SerializeField] float hpChangeDelay = 3f;
    bool isHpChanged = true;

    [SerializeField] private float speed = 5f;
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 100);
    }

    [SerializeField] private float atk = 10f;
    public float Atk
    {
        get => atk;
        set => atk = value;
    }

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
    public void Damaged(float reduceHp)
    {
        if (isHpChanged)
        {
            if (currentHp <= 0)
            {
                currentHp = 0;
                return;
            }

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

    private void Death()
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
