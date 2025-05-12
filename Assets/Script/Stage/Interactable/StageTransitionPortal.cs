using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTransitionPortal : MonoBehaviour
{
    [SerializeField] private string SceneName;    //이동할 씬 이름
    [SerializeField] protected LayerMask playerLayer;    //플레이어 레이어

    private void Start()
    {
        if (SceneName == null)
        {
            Debug.LogWarning(gameObject.name + ": 이동할 씬 이름을 입력해주세요");
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
