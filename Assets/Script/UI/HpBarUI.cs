using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    private Image hpBar;

    BaseStat baseStat;

    private void Awake()
    {
        baseStat = this.GetComponentInParent<BaseStat>();
        hpBar = this.GetComponent<Image>();
        baseStat.OnHpChanged += OnHpChanged;
    }

    private void OnDestroy()
    {
        baseStat.OnHpChanged -= OnHpChanged;
    }

    private void Start()
    {
        UpdateHpBar();
    }

    private void OnHpChanged()
    {
        UpdateHpBar();
    }

    public void UpdateHpBar()
    {
        float hpRatio = baseStat.CurrentHp / baseStat.MaxHp;
        hpBar.fillAmount = hpRatio;
    }
}
