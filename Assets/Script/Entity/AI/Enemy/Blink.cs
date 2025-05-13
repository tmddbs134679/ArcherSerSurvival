using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public void OnFlashEnd()
    {
        gameObject.transform.root.GetComponent<OrgeStateMachine>().Animator.SetLayerWeight(1, 0f);
    }

}