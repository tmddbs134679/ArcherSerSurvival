using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] private EnemyStat enemyStat;
    [SerializeField] private Image hpBar;



    private void Awake()
    {
        if (enemyStat == null)
            enemyStat = GetComponentInParent<EnemyStat>();


    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnEnable()
    {
        UpdateHpBar();

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
            hpBar.fillAmount = enemyStat.CurrentHp / enemyStat.MaxHp;
        }
    }


}
