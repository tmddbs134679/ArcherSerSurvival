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
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        baseStat = this.GetComponentInParent<BaseStat>();
        hpBar = this.GetComponent<Image>();
        baseStat.OnHpChanged += OnHpChanged;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        baseStat.OnHpChanged -= OnHpChanged;

    }


    private void Start()
    {
        UpdateHpBar();
        Debug.Log("HpBar");


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
