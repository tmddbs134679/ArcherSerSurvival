using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : BaseUI
{

    [SerializeField] private Button[] rewardButtons;
    [SerializeField] private CanvasGroup[] rewardButtonCanvasGroup;
    [SerializeField] private Button[] rerollButtons;
    [SerializeField] private Text[] name;

    [SerializeField]
    bool aniflag = false;

    [Header("SlotBox")]
    public CanvasGroup slotBox;
    public float duration = 1f;

    public SkillLevelSystem skillLevelSystem;
    public GameObject[] skillPrefabs;

    public WeightedTable weightedTable;

    string[] key = new string[3];




    private void Awake()
    {
        slotBox = transform.Find("SlotBox").gameObject.GetComponent<CanvasGroup>();
        skillLevelSystem = GameManager.Instance.skillLevelSystem;
        skillPrefabs = Resources.LoadAll<GameObject>("Prefabs/Skill/Prefabs");

    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        slotBox.alpha = 0f;
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        //???????곌떽釉?????????곌떽釉붾??
        StartCoroutine(BaseFadeIn());

        key[0] = weightedTable.GetRandom();
        name[0].text = key[0];
        key[1] = weightedTable.GetRandom();
        name[1].text = key[1];
        key[2] = weightedTable.GetRandom();
        name[2].text = key[2];
    }
    private void OnDisable()
    {
        slotBox.alpha = 0f;
        aniflag = false;
        Time.timeScale = 1f;
        foreach (var obj in rerollButtons)
        {
            obj.gameObject.SetActive(true);
        }

    }

    private void Update()
    {
        if (aniflag == false)
        {
            if (fadeFlag == false)
            {
                aniflag = true;
                StartCoroutine(Emergence(slotBox));
            }
        }
    }


    public IEnumerator Emergence(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / duration);
            canvasGroup.alpha = t;

            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    public IEnumerator Retreat(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / duration);
            canvasGroup.alpha = 1f - t;

            yield return null;
        }

        canvasGroup.alpha = 0f;
        aniflag = false;
        Time.timeScale = 1f;
        UIManager.Instance.HideUI(gameObject.name);
    }



    private void Start()
    {


        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i; // ???嚥?? ???筌??????ш끽維??λ궔?
            rewardButtons[i].onClick.AddListener(() => SelectButton(index));
            rerollButtons[i].onClick.AddListener(() => ReRollButton(index));
        }
    }



    public void SelectButton(int index)
    {


        if (index == 0)
        {
            Augmenter(key[0]);
        }


        else if (index == 1)
        {
            Augmenter(key[1]);
        }

        else if (index == 2)
        {
            Augmenter(key[2]);
        }


        foreach (var skill in PlayerController.Instance.skillList)
        {
            skill.GetComponent<BaseSkill>().SetSkillData();
        }

        StartCoroutine(Retreat(gameObject.GetComponent<CanvasGroup>()));


        //gameObject.SetActive(false);


    }

    void Augmenter(string serialName)
    {
        if (skillLevelSystem.changedSkillData[serialName].level == 0)
        {
            GameObject go = null;
            foreach (var skill in skillPrefabs)
            {
                var skillComp = skill.GetComponent<ProjectileSkill>();
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

    public void ReRollButton(int index)
    {

        key[index] = weightedTable.GetRandom();
        name[index].text = key[index];
        rerollButtons[index].gameObject.SetActive(false);

        Debug.Log(index);
        rewardButtonCanvasGroup[index].alpha = 0;
        StartCoroutine(Emergence(rewardButtonCanvasGroup[index]));
        //gameObject.SetActive(false);

    }
}