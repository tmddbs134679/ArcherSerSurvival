using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTransitionPortal : MonoBehaviour
{
    [SerializeField] private string SceneName;    //�̵��� �� �̸�
    [SerializeField] protected LayerMask playerLayer;    //�÷��̾� ���̾�

    private void Start()
    {
        if (SceneName == null)
        {
            Debug.LogWarning(gameObject.name + ": �̵��� �� �̸��� �Է����ּ���");
            this.enabled = false;
        } 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            GameManager.Instance.NextRoom();
        }
    }
}
