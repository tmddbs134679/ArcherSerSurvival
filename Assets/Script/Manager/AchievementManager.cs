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

    private void Awake()
    {
        
        base.Awake();
        if (Instance == this)
        {
            enemyList = Resources.LoadAll<GameObject>("Prefabs/Entity/Enemy");
            bossList = Resources.LoadAll<GameObject>("Prefabs/Entity/Boss");


            foreach (var obj in enemyList)
            {
                goalKillCnt.Add(obj.name, 15);
                killCnt.Add(obj.name, 0);
            }

            foreach (var obj in bossList)
            {
                goalKillCnt.Add(obj.name, 1);
                killCnt.Add(obj.name, 0);
            }
        }

    }


}
