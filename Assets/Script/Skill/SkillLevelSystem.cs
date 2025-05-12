using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Image;

public class SkillLevelSystem : MonoBehaviour
{


    public ProjectileData[] skillDataObject;

    Dictionary<string, ProjectileData> skillData = new Dictionary<string, ProjectileData>();
    public Dictionary<string, ChangedSkillData> changedSkillData = new Dictionary<string, ChangedSkillData>();

    
    List<string> skillKey;


    private void Awake()
    {
        skillDataObject = Resources.LoadAll<ProjectileData>("Prefabs/Skill/Data");


        foreach(var temp in skillDataObject)
        {
            skillData.Add(temp.name.Substring(0, temp.name.Length - "Data".Length), temp);
        }
        /*
        skillData.Add("Axe", skillDataObject[0]);
        skillData.Add("Knife", skillDataObject[1]);
        skillData.Add("Meteo", skillDataObject[2]);
        */

        skillKey = skillData.Keys.ToList();


        foreach (string key in skillKey)
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



    private void Start()
    {
        
        
    }

    public void SkillLevelUp(string key)
    {

        changedSkillData[key].speed = skillData[key].speed + skillData[key].lvspeed * changedSkillData[key].level;
        changedSkillData[key].damage = skillData[key].damage + skillData[key].lvdamage * changedSkillData[key].level;
        changedSkillData[key].count = skillData[key].count + skillData[key].lvcount * changedSkillData[key].level;

        Debug.Log(changedSkillData[key].count);
        Debug.Log(changedSkillData[key]);
        changedSkillData[key].level += 1;
    }
}
