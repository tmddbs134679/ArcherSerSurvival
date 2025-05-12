using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardUI : BaseUI
{
    [SerializeField] private Button[] rewardButtons; // ?몄뒪?숉꽣??踰꾪듉 3媛??곌껐
    public SkillLevelSystem skillLevelSystem;
    public GameObject[] skillPrefabs;
    private void Awake()
    {
        skillLevelSystem = GameManager.Instance.skillLevelSystem;
        
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void Start()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i; // ?대줈? 臾몄젣 諛⑹?
            rewardButtons[i].onClick.AddListener(() => SelectButton(index));
        }
    }
    public void SelectButton(int index)
    {
        if (index == 0)
        {
            if (skillLevelSystem.changedSkillData["Axe"].level == 0)
            {
                Debug.Log(index);
                GameObject go = Instantiate(skillPrefabs[0]);
                go.transform.SetParent(GameObject.Find("Player").transform);
                PlayerController.Instance.skillList.Add(go);
                skillLevelSystem.changedSkillData["Axe"].level += 1;
            }
            else
            {
                skillLevelSystem.SkillLevelUp("Axe");
                Debug.Log(skillLevelSystem.changedSkillData["Axe"].level);
            }
        }
        else if (index == 1)
        {
            Debug.Log(index);
            if (skillLevelSystem.changedSkillData["Knife"].level == 0)
            {
                GameObject go = Instantiate(skillPrefabs[1]);
                go.transform.SetParent(GameObject.Find("Player").transform);
                skillLevelSystem.changedSkillData["Knife"].level += 1;
            }
            else
            {
                skillLevelSystem.SkillLevelUp("Knife");
            }
        }
        else if (index == 2)
        {
        }
        foreach (var skill in PlayerController.Instance.skillList)
        {
            skill.GetComponent<ProjectileSkill>().SetSkillData();
        }
        UIManager.Instance.HideUI(gameObject.name);
        //gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
}
