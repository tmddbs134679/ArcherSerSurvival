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
    //?????띾???잙갭큔???????⑤챷諭???癲ル슢캉???????몃뤀
    //1/1

    //NPC??醫딆쓧? ?????띾???잙갭큔???④낯?←춯?????嶺뚮Ĳ????????????

    //?꿔꺂???????????????????됲닓?꿔꺂??? ?꿔꺂?????용Ъ?
    // ?????嶺?獄?????????筌???????繹먮굞???????띾???잙갭큔???
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
