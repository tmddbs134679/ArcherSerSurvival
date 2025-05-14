using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementUI : BaseUI
{
    public Text text;
    private void OnEnable()
    {
        text.text = AchievementManager.Instance.killCnt["Doc"].ToString() + " / " + AchievementManager.Instance.goalKillCnt["Doc"].ToString();

        text.text = "";

        foreach (var key in AchievementManager.Instance.killCnt.Keys)
        {
            text.text += key + " : " + AchievementManager.Instance.killCnt[key].ToString() + " / " + AchievementManager.Instance.goalKillCnt[key].ToString() + "\n";
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
