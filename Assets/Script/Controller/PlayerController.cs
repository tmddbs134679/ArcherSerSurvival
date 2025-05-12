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

    void PlayerMove()
    {
        // 닷지중이면 이동 멈춤
        if (isDodging)
        {
            return;
        }
        
        // 인풋 분리
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        pRigidbody.velocity = movement * playerStat.Speed;

        isMoving = movement.magnitude > 0.1f;

        animator.SetBool("isMove", movement.magnitude > 0.1f);

        // 이동시에는 회전값 고정
        if (isMoving)
        {
            Rotate(movement);
            weaponController.RotateWeapon(-90f);
        }
        Dodge(movement);
    }

    void Dodge(Vector2 direction)
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDodging)
        {
            StartCoroutine(DodgeRoutine(direction, playerStat.dodgePower, playerStat.dodgeCoolTime));
        }
        
    }

    IEnumerator DodgeRoutine(Vector2 direction, float dodgeSpeed, float duration)
    {
        isDodging = true;
        pRigidbody.velocity = direction.normalized * dodgeSpeed;
        Debug.Log("Dodge!");

        yield return new WaitForSeconds(duration);

        pRigidbody.velocity = Vector2.zero;
        isDodging = false;
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
