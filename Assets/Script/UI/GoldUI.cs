using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : BaseUI
{
    [SerializeField]
    private PlayerResource playerResource;
    [SerializeField]
    private Text goldAmountText;
    private void Start()
    {
        playerResource = PlayerController.Instance.GetComponent<PlayerResource>();
        if(playerResource != null ) 
        {
            playerResource.OnGoldChanged += UpdateUI;
            Debug.Log("gold UI Init");
            UpdateUI();
        }
    }

    private void OnDestroy()
    {
        playerResource.OnGoldChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        int gold = playerResource.goldAmount;

        Debug.Log(gameObject.name);
        goldAmountText.text = gold.ToString();
    }
}
