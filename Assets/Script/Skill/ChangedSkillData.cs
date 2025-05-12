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
    public float speed;//?¤í”¼??
    [Range(0, 100)]
    public float damage;//?°ë?ì§€
    [Range(1, 20)]
    public float duration;//?ëª… ì£¼ê¸°
    public Color color;//?œí„°?˜ì˜ ì¡°ì–¸-ë¶„ë¦¬ ?´ì£¼?”ê²Œ ì¢‹ìŒ
    public ParticleSystem impactEffect;//?œí„°?˜ì˜ ì¡°ì–¸-ë¶„ë¦¬ ?´ì£¼?”ê²Œ ì¢‹ìŒ
    [Range(0, 1200)]
    public float rotateSpeed;
    [Range(0, 100)]
    public int count;//???¬ì´??ë°œì‚¬ê°?ˆ˜
    [Range(10, 360)]
    public float angle;//?¬ì‚¬ì²?ë°œì‚¬ê°?
    [Range(0, 10)]
    public float hormingStartDelay; //?€ê²?ì¶”ì  ?œì‘ ?œê°„
    [Range(0, 10)]
    public float hormingTurnDelay;//?€ê²Ÿì„ ?¥í•´ ë°©í–¥???Œë¦¬???œê°„

}
