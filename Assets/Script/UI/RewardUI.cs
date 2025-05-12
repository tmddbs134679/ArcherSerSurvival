using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : BaseUI
{

    [SerializeField] private Button[] rewardButtons; // �ν����Ϳ� ��ư 3�� ����

    public SkillLevelSystem skillLevelSystem;

    public GameObject[] skillPrefabs;


    private void Awake()
    {
        //Init_Active();
    }


    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i; // Ŭ���� ���� ����
            rewardButtons[i].onClick.AddListener(() => SelectButton(index));
        }
    }

    public void SelectButton(int index)
    {

        if(index == 0)
        {
            
            if (skillLevelSystem.changedSkillData["Axe"].level == 0)
            {
                Debug.Log(index);
                GameObject go = Instantiate(skillPrefabs[0]);
                go.transform.SetParent(GameObject.Find("Player").transform);

                skillLevelSystem.changedSkillData["Axe"].level += 1;

            }

            else
            {
                skillLevelSystem.SkillLevelUp("Axe");
            }
        }


        else if(index == 1)
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

        else if(index == 2)
        {

        }

        UIManager.Instance.HideUI(gameObject.name);

    }

}
