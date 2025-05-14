using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private WeaponController WeaponPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private ParticleSystem particle;
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
        particle = GetComponentInChildren<ParticleSystem>();

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
        // ???繞벿살탳???move ??얜Ŧ吏?x
        if (isDodging)
        {
            return;
        }

        Vector2 movement = PlayerInput();
        pRigidbody.velocity = movement * playerStat.Speed;

        isMoving = movement.magnitude > 0.1f;

        animator.SetBool("isMove", movement.magnitude > 0.1f);

        // ??嶺뚯쉳?????異???얜Ŧ吏?
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
            StartCoroutine(DodgeRoutine(direction, playerStat.DodgePower, playerStat.DodgeDuration, playerStat.DodgeCoolTime));
        }
    }

    IEnumerator DodgeRoutine(Vector2 direction, float dodgePower, float duration, float coolTime)
    {
        isDodging = true;
        animator.SetBool("IsDodge", true);
        particle.Play();
        animator.speed = 1/duration;
        playerStat.isInvincible = true;
        sfxControl.OnDodge();
        isDodgeCoolDown = true;

        float elapsed = 0f;
        Vector2 start = pRigidbody.position;
        Vector2 end = start + direction.normalized * dodgePower;

        while (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = elapsed / duration;
            Vector2 newPosition = Vector2.Lerp(start, end, t);
            pRigidbody.MovePosition(newPosition);
            yield return new WaitForFixedUpdate();
        }

        //??怨뺣룛 ??リ턁筌???
        isDodging = false;
        particle.Stop();
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

        if (isLeft)
        {
            particle.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            particle.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Portal"))
        {
            GameManager.Instance.NextRoom();
        }
    }

}
