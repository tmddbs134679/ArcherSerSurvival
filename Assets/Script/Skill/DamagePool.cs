using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePool : Singleton<DamagePool>
{

    public DamageText prefab;
    public Canvas worldCanvas;
    private Queue<DamageText> dmgPool = new Queue<DamageText>();
    private int lastExpandCount = 10;

    private void Start()
    {
       
        for (int i = 0; i < lastExpandCount; i++)
        {
            var obj = Instantiate(prefab, worldCanvas.transform);
            obj.gameObject.SetActive(false);
            dmgPool.Enqueue(obj);
        }
    }


    public DamageText Get()
    {
        if (dmgPool.Count == 0)
        {
         
            int expandCount = lastExpandCount * 2;
            for (int i = 0; i < expandCount; i++)
            {
                var obj = Instantiate(prefab, worldCanvas.transform);
                obj.gameObject.SetActive(false);
                dmgPool.Enqueue(obj);
            }

            lastExpandCount = expandCount;
        }

        var text = dmgPool.Dequeue();
        text.gameObject.SetActive(true);
        return text;
    }

    public void Release(DamageText dmgText)
    {
        dmgText.gameObject.SetActive(false);
        dmgPool.Enqueue(dmgText);
    }

    public void ShowDamageText(int dmg, Vector3 pos)
    {
        DamageText damageText = Get();
        damageText.gameObject.SetActive(true);
        damageText.Show(dmg, pos); 
    }

}
