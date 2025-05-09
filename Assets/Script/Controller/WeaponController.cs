using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Attack Info")]

    [SerializeField] private SpriteRenderer weaponRenderer;

    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }

    private Animator animator;

    private static readonly int isAttack = Animator.StringToHash("isAttack");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.speed = 1.8f;
    }

    public void FlipWeapon(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }


    public void AttackAni()
    {
        animator.SetTrigger(isAttack);
    }
    
}
