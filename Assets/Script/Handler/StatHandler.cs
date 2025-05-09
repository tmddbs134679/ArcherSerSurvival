using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private int hp = 10;
    public int Hp
    {
        get => hp;
        set => hp = Mathf.Clamp(value, 0, 100);
    }

}
