using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    // PlayerStat으로 클래스명 변경
    [SerializeField] private float hp = 10;
    public float Hp
    {
        get => hp;
        set => hp = Mathf.Clamp(value, 0, 100);
    }

    [SerializeField] private float speed = 5f;
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 100);
    }

    [SerializeField] private float atk = 10f;
    public float Atk
    {
        get => atk;
        set => atk = value;
    }
}
