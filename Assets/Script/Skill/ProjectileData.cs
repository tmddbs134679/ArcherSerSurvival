using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewProjectileData", menuName = "Data/Projectile")]
public class ProjectileData : ScriptableObject
{
    [Range(0, 100)]
    public float speed;
    [Range(0, 100)]
    public float lvspeed;
    [Range(0, 100)]
    public float damage;
    [Range(0, 100)]
    public float lvdamage;
    [Range(0, 100)]
    public int count;
    [Range(0, 100)]
    public int lvcount;
    [Range(1, 20)]
    public float duration;
    public Color color;
    public ParticleSystem impactEffect;
    [Range(0, 1200)]
    public float rotateSpeed;
    [Range(10, 360)]
    public float angle;
    [Range(0, 10)]
    public float hormingStartDelay;
    [Range(0, 10)]
    public float hormingTurnDelay;
}





