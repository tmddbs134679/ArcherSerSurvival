using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangedSkillData : MonoBehaviour
{
    /*
    public float speed;
    public float damage;
    public float duration;
    public Color color;
    public ParticleSystem impactEffect;
    public float rotateSpeed;
    public int count;
    public float angle;
    public float hormingStartDelay;
    public float hormingTurnDelay;
    */
    public int level;
    [Range(0, 100)]
    public float speed;//?�피??
    [Range(0, 100)]
    public float damage;//?��?지
    [Range(1, 20)]
    public float duration;//?�명 주기
    public Color color;//?�터?�의 조언-분리 ?�주?�게 좋음
    public ParticleSystem impactEffect;//?�터?�의 조언-분리 ?�주?�게 좋음
    [Range(0, 1200)]
    public float rotateSpeed;
    [Range(0, 100)]
    public int count;//???�이??발사�?��
    [Range(10, 360)]
    public float angle;//?�사�?발사�?
    [Range(0, 10)]
    public float hormingStartDelay; //?��?추적 ?�작 ?�간
    [Range(0, 10)]
    public float hormingTurnDelay;//?�겟을 ?�해 방향???�리???�간
}











