using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteModeFollowCam : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;


    private void Start()
    {
        target = PlayerController.Instance.gameObject.transform;
        //FindObjectOfType<PlayerController>().transform;
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
