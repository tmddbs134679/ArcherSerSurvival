using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTransitionPortal : MonoBehaviour
{
    [SerializeField] private string SceneName;    //?대룞?????대쫫
    [SerializeField] protected LayerMask playerLayer;    //?뚮젅?댁뼱 ?덉씠??

    private void Start()
    {
        if (SceneName == null)
        {
            Debug.LogWarning(gameObject.name + ": ?대룞?????대쫫???낅젰?댁＜?몄슂");
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
