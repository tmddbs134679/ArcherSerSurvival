using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTreasureChest : MonoBehaviour
{
    private readonly int IsOpen = Animator.StringToHash("IsOpen");
    [SerializeField] protected LayerMask playerLayer;           // �÷��̾� ���̾�
    Animator animator;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // GameManager�� ���� ���� Ŭ���� �������� ���� Ȯ�� (if ���� �߰�)
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            animator.SetTrigger(IsOpen);
            //�������� Ŭ����, ���� ó��
        }
    }
}
