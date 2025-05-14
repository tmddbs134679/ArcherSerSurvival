using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public void OnFlashEnd()
    {

        gameObject.transform.GetComponentInParent<OrgeStateMachine>().Animator.SetLayerWeight(1, 0f);
    }

}