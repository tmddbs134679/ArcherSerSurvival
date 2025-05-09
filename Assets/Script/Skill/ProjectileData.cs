using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData", menuName = "Data/Projectile")]

public class ProjectileData : ScriptableObject//공용느낌
{
    public float speed;
    public float damage;
    public float duration;
    public Color color;//구분
    public ParticleSystem impactEffect;//구분
    public float rotateSpeed;
    public int count;//갯수
    public float angle;//투사체 발사각

    public float angleDelay; //타겟팅 지연 시간
}
