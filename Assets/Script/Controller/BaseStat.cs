using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour
{
    [SerializeField] protected float maxHp = 10f;
    public float MaxHp
    {
        get => maxHp;
        set => maxHp = Mathf.Clamp(value, 0, 100);
    }
    [SerializeField] protected float currentHp;

    [SerializeField] protected float speed = 5f;
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 100);
    }

    [SerializeField] protected float atk = 10f;
    public float Atk
    {
        get => atk;
        set => atk = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Damaged(float reduceHp)
    {

        if (currentHp <= 0)
        {
            currentHp = 0;
            return;
        }

        currentHp -= reduceHp;


        //if (currentHp <= 0)
        //{
        //    Death();
        //}


    }

    public virtual void Healed(float healHP)
    {
        currentHp += healHP;
        if(currentHp > maxHp) 
        {
            currentHp = maxHp;
        }
    }

    protected virtual void Death()
    {

    }
}
