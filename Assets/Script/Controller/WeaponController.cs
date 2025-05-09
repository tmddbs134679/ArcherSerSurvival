using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float power = 1f;

    [SerializeField] private SpriteRenderer weaponRenderer;

    [SerializeField] private GameObject projectilePrefab;
    
    private void Awake()
    {

    }

    public void RotateWeapon(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }

}
