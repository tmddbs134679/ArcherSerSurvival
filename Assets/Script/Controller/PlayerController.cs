using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private WeaponController WeaponPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private WeaponController weaponController;

    private Rigidbody2D pRigidbody;
    private Animator animator;

    public bool isMoving = false;

    private PlayerStat playerStat;

    public List<GameObject> skillList = new List<GameObject>();

    private bool isDodging = false;
    private bool isDodgeCoolDown = false;

    public bool isDialog = false;
    void Awake()
    {
        base.Awake();
        pRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerStat = GetComponent<PlayerStat>();

        if (WeaponPrefab != null)
            weaponController = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponController = GetComponentInChildren<WeaponController>();
    }

    void Update()
    {
        PlayerMove();
    }

    Vector2 PlayerInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void PlayerMove()
    {
        // 닷지중이면 이동 멈춤
        if (isDodging)
        {
            return;
        }
        
        Vector2 movement = PlayerInput();
        pRigidbody.velocity = movement * playerStat.Speed;

        isMoving = movement.magnitude > 0.1f;

        animator.SetBool("isMove", movement.magnitude > 0.1f);

        // 이동시에는 회전값 고정
        if (isMoving)
        {
            Rotate(movement);
            weaponController.RotateWeapon(-90f);
            Dodge(movement);
        }
        
    }

    void Dodge(Vector2 direction)
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDodging && !isDodgeCoolDown)
        {
            StartCoroutine(DodgeRoutine(direction, playerStat.DodgeSpeed, playerStat.DodgeDuration, playerStat.DodgeCoolTime));
        }
        animator.SetBool("IsDodge", isDodging);
        

    }

    IEnumerator DodgeRoutine(Vector2 direction, float dodgeSpeed, float duration, float coolTime)
    {
        isDodging = true;
        animator.speed = dodgeSpeed / 2;
        playerStat.isInvincible = true;

        isDodgeCoolDown = true;
        pRigidbody.velocity = direction.normalized * dodgeSpeed;

        yield return new WaitForSeconds(duration);

        isDodging = false;
        playerStat.isInvincible = false;

        // 회피 종료 후 이동 반영
        animator.speed = 1f;
        Vector2 movement = PlayerInput();
        pRigidbody.velocity = movement * playerStat.Speed;

        yield return new WaitForSeconds(coolTime);
        isDodgeCoolDown = false;
    }

    void Rotate(Vector2 direction)
    {
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool isLeft = Mathf.Abs(rotation) > 90f;

        spriteRenderer.flipX = isLeft;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Portal"))
        {
            GameManager.Instance.NextRoom();
        }
    }

}
