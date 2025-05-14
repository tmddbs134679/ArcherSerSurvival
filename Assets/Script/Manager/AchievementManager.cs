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
    //???リ퐛?洹먮봾?????곸쓤???筌먦끇????용뉴
    //1/1

    //NPC?띠럾? ???リ퐛?洹먮봾?곁춯琉?????類ㅼ떨?????뗮닡??

    //嶺뚮씞???????????????덈츎嶺뚯솘? 嶺뚳퐢?얍칰?
    // ?????戮?뱺 ????????뽯윞?????깅쾳 ???リ퐛?洹먮봾??
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

        achievementEvent.Add("Doc", "Doc사냥꾼");
        achievementFlag.Add("Doc", false);
        achievementEvent.Add("Skelet", "해골그잡채");
        achievementFlag.Add("Skelet", false);


    }


}
