using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTrap : MonoBehaviour   //�÷��̾� ���� �� ���ظ� ������ ����
{
    [Header("Trap Settings(Base)")]
    [SerializeField] protected float damageAmount = 10f;        // ���ط�
    [SerializeField] protected LayerMask playerLayer;           // ���� ��� ���̾�(�÷��̾�)

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            //������ ó��
        }
    }

}

