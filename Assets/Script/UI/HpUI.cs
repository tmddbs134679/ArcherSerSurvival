using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] private EnemyStat enemyStat;
    [SerializeField] Slider slider;

    private void Awake()
    {
     
    }

    private void OnEnable()
    {
        if (enemyStat != null)
        {
            enemyStat.OnTakeDamage += UpdateHpBar;
        }
    }

    private void OnDisable()
    {
        if (enemyStat != null)
        {
            enemyStat.OnTakeDamage -= UpdateHpBar;
        }
    }

    private void UpdateHpBar()
    {
        if (enemyStat != null)
        {
            slider.value = enemyStat.CurrentHp / enemyStat.MaxHp;
        }
    }
}
