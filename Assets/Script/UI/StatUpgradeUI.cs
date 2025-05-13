using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeUI : BaseUI
{
    [SerializeField] StatUpgrade statUpgrade;
    [SerializeField] Button CloseButton;
    PlayerStat playerStat;
    PlayerResource playerResource;

    [Header("Upgrade Button")]
    [SerializeField] private Button healthUpgradeButton;
    [SerializeField] private Button attackUpgradeButton;
    [SerializeField] private Button speedUpgradeButton;

    [Header("Current Stat")]
    [SerializeField] private Text currentHealth;
    [SerializeField] private Text currentAttack;
    [SerializeField] private Text currentSpeed;
    
    [Header("Upgrade Amount")]
    [SerializeField] private Text healthUpgradeAmount;
    [SerializeField] private Text attackUpgradeAmount;
    [SerializeField] private Text speedUpgradeAmount; 
    
    [Header("Upgrade Cost")]
    [SerializeField] private Text healthUpgradeCost;
    [SerializeField] private Text attackUpgradeCost;
    [SerializeField] private Text speedUpgradeCost;



    private void Start()
    {
        playerStat = PlayerController.Instance.GetComponent<PlayerStat>();
        playerResource = PlayerController.Instance.GetComponent<PlayerResource>();
        if(playerStat == null || playerResource == null ) 
        {
            Debug.LogWarning("Can't Find Required Player Component");
            return;
        }
        UpdateUI();

        healthUpgradeButton.onClick.AddListener(statUpgrade.UpgradeHealth);
        attackUpgradeButton.onClick.AddListener(statUpgrade.UpgradeAttack);
        speedUpgradeButton.onClick.AddListener(statUpgrade.UpgradeSpeed);

        CloseButton.onClick.AddListener(CloseUI);
        playerStat.OnStatChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        currentHealth.text = playerStat.MaxHp.ToString();
        healthUpgradeAmount.text = "+ " + statUpgrade.HealthUpgradeAmount.ToString();
        healthUpgradeCost.text = statUpgrade.HealthUpgradeCost.ToString() + "G";

        currentAttack.text = playerStat.Atk.ToString();
        attackUpgradeAmount.text =  "+ " +statUpgrade.AttackUpgradeAmount.ToString();
        attackUpgradeCost.text = statUpgrade.AttackUpgradeCost.ToString() + "G";
        
        currentSpeed.text = playerStat.Speed.ToString();
        speedUpgradeAmount.text = "+ " + statUpgrade.SpeedUpgradeAmount.ToString();
        speedUpgradeCost.text = statUpgrade.SpeedUpgradeCost.ToString() + "G"; 
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
