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
    private PlayerSFXControl sfxControl;
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
        sfxControl = GetComponent<PlayerSFXControl>();

        if (WeaponPrefab != null)
            weaponController = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponController = GetComponentInChildren<WeaponController>();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    Vector2 PlayerInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void PlayerMove()
    {
        // ?룹?以묒씪??move ?묐룞 x
        if (isDodging)
        {
            return;
        }

        Vector2 movement = PlayerInput();
        pRigidbody.velocity = movement * playerStat.Speed;

        isMoving = movement.magnitude > 0.1f;

        animator.SetBool("isMove", movement.magnitude > 0.1f);

        // ?吏곸씪?뚮쭔 ?묐룞
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
    }

    IEnumerator DodgeRoutine(Vector2 direction, float dodgeSpeed, float duration, float coolTime)
    {
        isDodging = true;
        animator.SetBool("IsDodge", true);
        animator.speed = dodgeSpeed / 2f;
        playerStat.isInvincible = true;
        sfxControl.OnDodge();
        isDodgeCoolDown = true;

        float elapsed = 0f;
        Vector2 start = pRigidbody.position;
        Vector2 end = start + direction.normalized * dodgeSpeed;

        while (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = elapsed / duration;
            Vector2 newPosition = Vector2.Lerp(start, end, t);
            pRigidbody.MovePosition(newPosition);
            yield return new WaitForFixedUpdate();
        }

        //?뚰뵾 醫낅즺 ??
        isDodging = false;
        animator.SetBool("IsDodge", false);
        animator.speed = 1f;
        playerStat.isInvincible = false;

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
