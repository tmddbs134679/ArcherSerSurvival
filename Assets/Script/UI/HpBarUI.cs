using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : BaseUI
{
    [SerializeField] private Image hpBar;

    [SerializeField] BaseStat baseStat;

    private void Awake()
    {
        baseStat = PlayerController.Instance.GetComponent<BaseStat>();
    }
    private void Start()
    {
        UpdateHpBar();
        Debug.Log("HpBar");

        baseStat.OnHpChanged += OnHpChanged;
    }

    private void OnHpChanged(float currentHp, float maxHp)
    {
        UpdateHpBar();
    }

    public void UpdateHpBar()
    {
        float hpRatio = baseStat.CurrentHp / baseStat.MaxHp;
        hpBar.fillAmount = hpRatio;
    }
}
