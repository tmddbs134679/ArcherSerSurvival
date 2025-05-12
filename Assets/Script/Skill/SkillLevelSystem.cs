using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillLevelSystem : MonoBehaviour
{

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


    public ProjectileData[] skillDataObject;

    Dictionary<string, ProjectileData> skillData = new Dictionary<string, ProjectileData>();
    public Dictionary<string, ChangedSkillData> changedSkillData = new Dictionary<string, ChangedSkillData>();

    
    List<string> skillKey;


    private void Awake()
    {
        skillData.Add("Axe", skillDataObject[0]);
        skillData.Add("Knife", skillDataObject[1]);
    }
    

    
    private void Start()
    {
        skillKey = skillData.Keys.ToList();


        foreach(string key in skillKey)
        {
            ChangedSkillData newData = new ChangedSkillData();


            newData.speed = skillData[key].speed;
            newData.damage = skillData[key].damage;
            newData.duration = skillData[key].duration;
            newData.color = skillData[key].color;
            newData.impactEffect = skillData[key].impactEffect;
            newData.rotateSpeed = skillData[key].rotateSpeed;
            newData.count = skillData[key].count;
            newData.angle = skillData[key].angle;
            newData.hormingStartDelay = skillData[key].hormingStartDelay;
            newData.hormingTurnDelay = skillData[key].hormingTurnDelay;
            changedSkillData.Add(key, newData);
        }
        
    }

    public void SkillLevelUp(string key)
    {

        changedSkillData[key].speed = skillData[key].speed + skillData[key].lvspeed * changedSkillData[key].level;
        changedSkillData[key].damage = skillData[key].damage + skillData[key].lvdamage * changedSkillData[key].level;
        changedSkillData[key].count = skillData[key].count + skillData[key].lvcount * changedSkillData[key].level;

        Debug.Log(changedSkillData[key].count);

        changedSkillData[key].level += 1;
    }
}
