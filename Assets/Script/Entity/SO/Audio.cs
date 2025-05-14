using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio", fileName = "Audio")]
public class Audio : ScriptableObject
{
    public EENEMYTYPE monsterType;
    public AudioClip damage;
    public AudioClip die;
    public AudioClip move;

    public AudioClip[] weaponSounds; 
}
