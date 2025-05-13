using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Image hpBar;

    [SerializeField] BaseStat baseStat;

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
        baseStat = PlayerController.Instance.GetComponent<BaseStat>();
        hpBar = gameObject.transform.Find("Mask").transform.Find("HPBar").gameObject.GetComponent<Image>();
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
        Debug.Log(gameObject.name +" : "+ hpBar);

        //hpBar.fillAmount = hpRatio;
    }
}
