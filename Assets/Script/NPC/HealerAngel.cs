using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAngel : MonoBehaviour
{
    [SerializeField] private int healCost = 500;
    NPCController controller;
    
    private void Start()
    {
        controller = GetComponent<NPCController>();
        if(controller != null)
        {
            controller.onDialogEndEvent.AddListener(Heal);
        }
    }

    public void Heal(GameObject player)
    {
        if (player.GetComponent<PlayerResource>().UseGold(healCost))
        {
            player.GetComponent<PlayerStat>()?.Healed(10000);
        }
    }
}
