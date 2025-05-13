using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Transform target;       // 떨어질 타겟
    public float fallSpeed = 10f;  // 떨어지는 속도
    public GameObject impactEffect; // 충돌 이펙트 (옵션)

    private Vector3 targetPosition;

    void Start()
    {
        if (target != null)
        {
            targetPosition = target.position;
        }
        else
        {
            Debug.LogWarning("타겟이 설정되지 않았습니다.");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 타겟 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallSpeed * Time.deltaTime);

        // 도착하면 충돌 처리
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (impactEffect != null)
            {
                Instantiate(impactEffect, targetPosition, Quaternion.identity);
            }

            // 타겟에게 데미지 주는 코드 등 추가 가능
            Destroy(gameObject);
        }
    }
}
