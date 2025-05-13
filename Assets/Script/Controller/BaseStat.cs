using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour
{
    // 이벤트로 HP 바 변경 호출
    public event Action<float, float> OnHpChanged;
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
            OnHpChanged?.Invoke(currentHp, maxHp);
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

  

    //hp 초기화
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
