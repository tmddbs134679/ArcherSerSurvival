using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundManager : Singleton<MonsterSoundManager>
{
    [SerializeField] private List<Audio> monsterAudioSets;

    private Dictionary<EENEMYTYPE, Audio> monsterAudioDic;
    protected override void Awake()
    {
        base.Awake();

        monsterAudioDic = new Dictionary<EENEMYTYPE, Audio>();
        foreach (var set in monsterAudioSets)
        {
            monsterAudioDic[set.monsterType] = set;
        }
    }

    public void PlayMonsterDie(EENEMYTYPE type, AudioSource source)
    {
        if (monsterAudioDic.TryGetValue(type, out var set) && set.die != null)
        {
            source.PlayOneShot(set.die);
        }
    }

    public void PlayMonsterDamage(EENEMYTYPE type, AudioSource source)
    {
        if (monsterAudioDic.TryGetValue(type, out var set) && set.damage != null)
        {
            source.PlayOneShot(set.damage);
        }
    }

}
