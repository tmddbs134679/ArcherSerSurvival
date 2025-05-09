using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData", menuName = "Data/Projectile")]

public class ProjectileData : ScriptableObject
{
    public float speed;
    public float damage;
    public float duration;
    public Color color;
    public ParticleSystem impactEffect;
    public float rotateSpeed;
    public int count;
    public float angle;

    public float angleDelay;
}
