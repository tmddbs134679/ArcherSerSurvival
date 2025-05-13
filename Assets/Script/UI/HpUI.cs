using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] private EnemyStat enemyStat;
    [SerializeField] private Slider slider;



    private void Awake()
    {
        if (enemyStat == null)
            enemyStat = GetComponentInParent<EnemyStat>();

        
    }

    private void OnEnable()
    {
        if (enemyStat != null)
            enemyStat.OnTakeDamage += UpdateHpBar;
    }

    private void OnDisable()
    {
        if (enemyStat != null)
            enemyStat.OnTakeDamage -= UpdateHpBar;
    }

    private void UpdateHpBar()
    {
     
        if (enemyStat != null)
        {
            slider.value = enemyStat.CurrentHp / enemyStat.MaxHp;
        }
    }
}
