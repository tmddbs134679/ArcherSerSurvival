using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementKnight : MonoBehaviour
{
    NPCController controller;

    private void Start()
    {
        controller = GetComponent<NPCController>();
        if (controller != null)
        {
            controller.onDialogEndEvent.AddListener(ShowAchievement);
        }
    }

    public void ShowAchievement(GameObject player)
    {
        UIManager.Instance.ShowUI("Achievement");
    }
}
