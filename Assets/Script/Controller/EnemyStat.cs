using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : BaseStat
{

    public event Action OnTakeDamage;
    public event Action OnDie;
    // Start is called before the first frame update

    private void OnEnable()
    {
        currentHp = maxHp;
    }
 

    // Update is called once per frame
    void Update()
    {

    }

    public override void Damaged(float reduceHp)
    {
        base.Damaged(reduceHp);

        OnTakeDamage?.Invoke();
        

        if (currentHp == 0)
        {
            OnDie?.Invoke();

        }
    }

}