using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : BaseUI
{

    [SerializeField] private Button[] rewardButtons; // ??耀붾굝?????????????????????3?????????ㅻ깹???

    public SkillLevelSystem skillLevelSystem;

    public GameObject[] skillPrefabs;


    private void Awake()
    {
        skillLevelSystem = GameManager.Instance.skillLevelSystem;
        skillPrefabs = Resources.LoadAll<GameObject>("Prefabs/Skill/Prefabs");
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        //??????볥굜???????볥궚??
        StartCoroutine(BaseFadeIn());
    }

    private void Start()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i; // ?????? ?????????????諛몃마??潁뺛깺苡?
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
            }
        }


        else if (index == 1)
        {
            if (skillLevelSystem.changedSkillData["Knife"].level == 0)
            {
                GameObject go = Instantiate(skillPrefabs[1]);
                go.transform.SetParent(GameObject.Find("Player").transform);

                PlayerController.Instance.skillList.Add(go);

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
