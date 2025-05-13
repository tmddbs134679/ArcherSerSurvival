using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Transform target;
    private Vector3 fixedXZ;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveMinY = 1f;
    [SerializeField] private float moveMaxY = 6.15f;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        fixedXZ = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void LateUpdate()
    {
        float targetY = Mathf.Clamp(target.position.y, moveMinY, moveMaxY);
        float lerpY = Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * moveSpeed);

        transform.position = new Vector3(fixedXZ.x, lerpY, fixedXZ.z);
    }
}
