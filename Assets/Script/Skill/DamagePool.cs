using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePool : Singleton<DamagePool>
{

    public DamageText prefab;
    public Canvas worldCanvas;
    private Queue<DamageText> dmgPool = new Queue<DamageText>();
    public DamageText Get()
    {
        if (dmgPool.Count > 0)
        {
            var t = dmgPool.Dequeue();
            t.gameObject.SetActive(true);
            return t;
        }

        var instance = Instantiate(prefab, worldCanvas.transform);
        return instance;
    }
    public void Release(DamageText text)
    {
        text.gameObject.SetActive(false);
        dmgPool.Enqueue(text);
    }

    public void ShowDamageText(int damage, Vector3 position)
    {
        DamageText damageText = Get();
        damageText.gameObject.SetActive(true);
        damageText.Show(damage, position); 
    }

}
