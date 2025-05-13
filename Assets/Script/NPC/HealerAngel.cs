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
            PlayerResource player = controller.GetComponent<PlayerResource>();
            if(player != null) 
            {
                if (player.UseGold(healCost))
                {
                    controller.onDialogEndEvent.AddListener(Heal);
                }   
            }
        }
    }

    public void Heal(GameObject player)
    {
        player.GetComponent<PlayerStat>()?.Healed(10000);
    }
}
