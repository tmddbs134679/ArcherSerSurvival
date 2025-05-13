using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : BaseUI
{

    [SerializeField] private Button[] rewardButtons;

    public SkillLevelSystem skillLevelSystem;

    public GameObject[] skillPrefabs;

    public WeightedTable weightedTable;
    private void Awake()
    {
        skillLevelSystem = GameManager.Instance.skillLevelSystem;
        skillPrefabs = Resources.LoadAll<GameObject>("Prefabs/Skill/Prefabs");
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i; // ??以?? ?얜챷??獄쎻뫗?
            rewardButtons[i].onClick.AddListener(() => SelectButton(index));
        }
    }

    public void SelectButton(int index)
    {

        if (index == 0)
        {
            Augmenter(weightedTable.GetRandom());
        }

        else if (index == 1)
        {
            Augmenter(weightedTable.GetRandom());
        }

        else if (index == 2)
        {
            Augmenter(weightedTable.GetRandom());
        }


        foreach (var skill in PlayerController.Instance.skillList)
        {
            skill.GetComponent<ProjectileSkill>().SetSkillData();
        }
        UIManager.Instance.HideUI(gameObject.name);
        //gameObject.SetActive(false);

        Time.timeScale = 1f;
    }

    void Augmenter(string serialName)
    {
        if (skillLevelSystem.changedSkillData[serialName].level == 0)
        {
            GameObject go = null;
            foreach (var skill in skillPrefabs)
            {
                var skillComp = skill.GetComponent<BaseSkill>();
                Debug.Log(skillComp.serialname);
                if (skillComp != null && serialName == skillComp.serialname)
                {
                    go = Instantiate(skill);
                    break;
                }
            }
            go.transform.SetParent(GameObject.Find("Player").transform);
            PlayerController.Instance.skillList.Add(go);
            skillLevelSystem.changedSkillData[serialName].level += 1;
        }
        else
        {
            skillLevelSystem.SkillLevelUp(serialName);
        }
    }

}
