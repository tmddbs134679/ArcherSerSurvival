using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour
{
    // ???繹?筌뤾퍔夷?HP ???곌떠????筌뤾쑵??
    public Action OnHpChanged;
    public event Action OnStatChanged;

    [SerializeField] protected float maxHp = 100f;
    public float MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = Mathf.Clamp(value, 0, 1000);
            OnStatChanged?.Invoke();
        }
    }
    [SerializeField] protected float currentHp;
    public float CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = Mathf.Clamp(value, 0, maxHp);
            OnHpChanged?.Invoke();
        }
    }

    [SerializeField] protected float speed = 5f;
    public float Speed
    {
        get => speed;
        set
        {
            speed = Mathf.Clamp(value, 0, 100);
            OnStatChanged?.Invoke();
        }
    }

    [SerializeField] protected float atk = 10f;
    public float Atk
    {
        get => atk;
        set
        {
            atk = value;
            OnStatChanged?.Invoke();
        }
    }

  

    //hp ?貫?껆뵳??
    private void Start()
    {
        CurrentHp = maxHp;
    }

    public virtual void Damaged(float reduceHp)
    {

        if (currentHp <= 0)
        {
            CurrentHp = 0;
            return;
        }

        CurrentHp -= reduceHp;
    }

    public virtual void Healed(float healHP)
    {
        CurrentHp += healHP;
        if(currentHp > maxHp) 
        {
            CurrentHp = maxHp;
        }
    }

    protected virtual void Death()
    {

    }
}
