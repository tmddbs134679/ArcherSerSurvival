using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    [SerializeField] SpriteRenderer doorOpenedSprite;
    [SerializeField] SpriteRenderer doorClosedSprite;
    private StageTransitionPortal transitionPortal;
    //GameManager gameManger

    void Start()
    {
        Init();
        CloseDoor();
        //gameManager.OnGameOver += OpenDoor    
    }

    void Init()
    {
        //gameManager = GameManager.Instance;
        transitionPortal = GetComponent<StageTransitionPortal>();
    }

    public void OpenDoor()
    {
        doorClosedSprite.enabled = false;
        doorOpenedSprite.enabled = true;
        transitionPortal.enabled = true;
    }

    public void CloseDoor()
    {
        doorClosedSprite.enabled = true;
        doorOpenedSprite.enabled = false;
        transitionPortal.enabled = false;
    }
}
