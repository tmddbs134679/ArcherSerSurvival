using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTrap : MonoBehaviour   //�÷��̾� ���� �� ���ظ� ������ ����
{
    [Header("Trap Settings(Base)")]
    [SerializeField] protected float damageAmount = 10f;        // ���ط�
    [SerializeField] protected LayerMask playerLayer;           // ���� ��� ���̾�(�÷��̾�)
    //protected PlayerResource player  ������ ó���� ���� �÷��̾� ü�� ���� ��ü

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            //player = other.GetComponent<PlayerResource>();
            TryDealDamage();
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            TryDealDamage();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        /*
         * if(player != null && other.gameObject = player.gameObject){
         *      player = null;
         */
    }

    protected void TryDealDamage()
    {
        //if(player == null) return;
        //������ ó��
    }
}

