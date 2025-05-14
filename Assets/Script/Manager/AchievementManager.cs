using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    public int cnt = 0;
    public int golaCnt = 15;
    public GameObject[] enemyList;
    public GameObject[] bossList;

    public Dictionary<string, int> goalKillCnt = new Dictionary<string, int>();
    public Dictionary<string, int> killCnt = new Dictionary<string, int>();
    public Dictionary<string, int> currentKillCnt = new Dictionary<string, int>();


    public Dictionary<string, bool> achievementFlag = new Dictionary<string, bool>();
    public Dictionary<string, string> achievementEvent = new Dictionary<string, string>();

    public string currentKey;
    //????ろ맀?域밸Ŧ遊?????怨몄뱾???嶺뚮Ĳ??????⑸돱
    //1/1

    //NPC??좊읈? ????ろ맀?域밸Ŧ遊?怨곸땡筌?????筌먦끉????????떋??

    //癲ル슢?????????????????덉툗癲ル슣?? 癲ル슪???띿물?
    // ?????筌?諭?????????戮?쐾?????源낆쓱 ????ろ맀?域밸Ŧ遊??
    private void Awake()
    {
        
        base.Awake();
        if (Instance == this)
        {
            enemyList = Resources.LoadAll<GameObject>("Prefabs/Entity/Enemy");
            bossList = Resources.LoadAll<GameObject>("Prefabs/Entity/Boss");


            foreach (var obj in enemyList)
            {
                goalKillCnt.Add(obj.name, 3);
                killCnt.Add(obj.name, 0);
            }

            foreach (var obj1 in bossList)
            {
                goalKillCnt.Add(obj1.name, 1);
                killCnt.Add(obj1.name, 0);
            }
        }

        achievementEvent.Add("Doc", "Doc Hunter");
        achievementFlag.Add("Doc", false);
        achievementEvent.Add("Skelet", "Skelelelelelet");
        achievementFlag.Add("Skelet", false);


    }


}
