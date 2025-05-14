using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : BaseUI
{

    [SerializeField] private Button[] rewardButtons;
    [SerializeField] private CanvasGroup[] rewardButtonCanvasGroup;
    [SerializeField] private Button[] rerollButtons;
    public Sprite[] images;

    public Dictionary<string, Sprite> weaponImage = new Dictionary<string, Sprite>();
    //test
    [SerializeField]
    private Image[] icon;
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

    bool flag = false;

    //?????????????????????諛몄カ????????
    private void Awake()
    {
        
        weaponImage.Add("Axe", images[0]);
        weaponImage.Add("Knife", images[1]);
        weaponImage.Add("Arrow", images[2]);
        weaponImage.Add("Ice", images[3]);
        
        slotBox = transform.Find("SlotBox").gameObject.GetComponent<CanvasGroup>();
        skillLevelSystem = GameManager.Instance.skillLevelSystem;
        skillPrefabs = Resources.LoadAll<GameObject>("Prefabs/Skill/Prefabs");

    }

    private void OnEnable()
    {
        
        slotBox.alpha = 0f;
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        StartCoroutine(BaseFadeIn());


        for (int i = 0; i < 3; i++)
        {
            key[i] = weightedTable.GetRandom();
            name[i].text = key[i];
            icon[i].sprite = weaponImage[key[i]];
        }



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
            int index = i; // ?????? ?????????????????썹땟戮녹??諭?????⑸㎦??????蹂㏓???????????????
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
                var skillComp = skill.GetComponent<BaseSkill>();
                if (skillComp != null && serialName == skillComp.serialname)
                {
                    go = Instantiate(skill);
                    break;
                }
            }
            go.transform.SetParent(PlayerController.Instance.transform);
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
        icon[index].sprite = weaponImage[key[index]];
        rerollButtons[index].gameObject.SetActive(false);

        Debug.Log(index);
        rewardButtonCanvasGroup[index].alpha = 0;
        StartCoroutine(Emergence(rewardButtonCanvasGroup[index]));
        //gameObject.SetActive(false);

    }
}