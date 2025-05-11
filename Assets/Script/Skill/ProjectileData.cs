using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData", menuName = "Data/Projectile")]

public class ProjectileData : ScriptableObject//공용느낌
{
    [Range(0, 100)]
    public float speed;//스피드
    [Range(0, 100)]
    public float damage;//데미지
    [Range(1,20)]
    public float duration;//생명 주기
    public Color color;//튜터님의 조언-분리 해주는게 좋음
    public ParticleSystem impactEffect;//튜터님의 조언-분리 해주는게 좋음
    [Range(0,1200)]
    public float rotateSpeed;
    [Range(0, 100)]
    public int count;//한 사이클 발사갯수
    [Range(10,360)]
    public float angle;//투사체 발사각
    [Range(0, 10)]
    public float hormingStartDelay; //타겟 추적 시작 시간
    [Range(0, 10)]
    public float hormingTurnDelay;//타겟을 향해 방향을 돌리는 시간

}
