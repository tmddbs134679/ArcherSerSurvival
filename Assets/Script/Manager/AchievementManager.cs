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
    //??쀫꽅?귐딅섰 ??놁읅???類ㅻ??댿봺
    //1/1

    //NPC揶쎛 ??쀫꽅?귐딅섰筌뤿?????뺤춳????る툡??

    //筌띲끇????癒?퐣 ??苑??덈뮉筌왖 筌ｋ똾寃?
    // ??苑??뽯퓠 ????苑??뉖럡????쇱벉 ??쀫꽅?귐딅섰
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

            foreach (var obj in bossList)
            {
                goalKillCnt.Add(obj.name, 1);
                killCnt.Add(obj.name, 0);
            }
        }

        achievementEvent.Add("Doc", "Doc?щ깷袁?");
        achievementFlag.Add("Doc", false);
        achievementEvent.Add("Sekelete", "?닿낏洹몄옟梨?");
        achievementFlag.Add("Sekelete", false);


    }


}
