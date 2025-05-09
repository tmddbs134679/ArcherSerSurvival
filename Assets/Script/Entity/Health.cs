using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;

    [SerializeField] private float health;

    public event Action OnTakeDamage;
    public event Action OnDie;
    public bool IsDead => health == 0;

 
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float dmg)
    {
        if (health == 0)
            return;

        health = Mathf.Max(health - dmg, 0);

        OnTakeDamage?.Invoke();

        if(health == 0)
        {
            OnDie?.Invoke();

        }

    }
}
