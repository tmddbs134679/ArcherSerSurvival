using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrade : MonoBehaviour
{
    NPCController controller;
    PlayerStat playerStat;
    PlayerResource playerResource;
    public int HealthUpgradeAmount { get; private set;} = 10;      //체력 강화량
    public int AttackUpgradeAmount{ get; private set;} = 4;      //공격력 강화량
    public int SpeedUpgradeAmount { get; private set;} = 2;       //이동속도 강화량

    public int HealthUpgradeCost { get; private set;}= 50;      //체력 강화량
    public int AttackUpgradeCost{ get; private set;} = 25;      //공격력 강화량
    public int SpeedUpgradeCost { get; private set;}= 100;       //이동속도 강화량

    private void Start()
    {
        controller = GetComponent<NPCController>();
        if (controller != null)
        {
            controller.onDialogEndEvent.AddListener(StartUpgrade);
        }
    }

    public void StartUpgrade(GameObject player)
    {
        playerStat = player.GetComponent<PlayerStat>();
        playerResource = player.GetComponent<PlayerResource>();
        if (playerStat == null || playerResource == null)
        {
            Debug.LogWarning("Can't Find Required Player Component");
            return;
        }
        UIManager.Instance.ShowUI("StatUpgradeUI");
    }

    public void UpgradeHealth()
    {
        if (playerResource.UseGold(HealthUpgradeCost))
        {
            playerStat.MaxHp += HealthUpgradeAmount;
        }
    }
    public void UpgradeAttack()
    {
        if (playerResource.UseGold(AttackUpgradeCost))
        {
            playerStat.Atk += AttackUpgradeAmount;
        }
    }
    public void UpgradeSpeed()
    {
        if (playerResource.UseGold(SpeedUpgradeCost))
        {
            playerStat.Speed += SpeedUpgradeAmount;
        }
    }

}
