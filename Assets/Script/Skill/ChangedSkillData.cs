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
    public float speed;//?占쏀뵾??
    [Range(0, 100)]
    public float damage;//?占쏙옙?吏
    [Range(1, 20)]
    public float duration;//?占쎈챸 二쇨린
    public Color color;//?占쏀꽣?占쎌쓽 議곗뼵-遺꾨━ ?占쎌＜?占쎄쾶 醫뗭쓬
    public ParticleSystem impactEffect;//?占쏀꽣?占쎌쓽 議곗뼵-遺꾨━ ?占쎌＜?占쎄쾶 醫뗭쓬
    [Range(0, 1200)]
    public float rotateSpeed;
    [Range(0, 100)]
    public int count;//???占쎌씠??諛쒖궗占?占쏙옙
    [Range(10, 360)]
    public float angle;//?占쎌궗占?諛쒖궗占?
    [Range(0, 10)]
    public float hormingStartDelay; //?占쏙옙?異붿쟻 ?占쎌옉 ?占쎄컙
    [Range(0, 10)]
    public float hormingTurnDelay;//?占쎄쿊???占쏀빐 諛⑺뼢???占쎈━???占쎄컙
}











