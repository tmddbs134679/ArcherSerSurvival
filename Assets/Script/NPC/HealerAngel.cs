using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAngel : MonoBehaviour
{
    NPCController controller;

    private void Start()
    {
        controller = GetComponent<NPCController>();
        if(controller != null)
        {
            //플레이어 골드 소모
            controller.onDialogEndEvent.AddListener(Heal);
        }
    }

    public void Heal(GameObject player)
    {
        player.GetComponent<PlayerStat>()?.Healed(10000);
    }
}
