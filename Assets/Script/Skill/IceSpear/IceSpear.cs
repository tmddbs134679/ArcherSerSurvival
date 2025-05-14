using UnityEngine;

public class IceSpear : MonoBehaviour
{
    public float fallSpeed = 10f;  // 떨어지는 속도

    public Vector3 targetPosition;
    void Update()
    {
        // 타겟 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallSpeed * Time.deltaTime);
        // 도착하면 충돌 처리
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {

            
              // Instantiate(impactEffect,transform.position, Quaternion.identity);
                     GameObject IceWave =ProjectileObjectPool.Instance.Get("IceWave");
                     IceWave.transform.position=transform.position;
           ProjectileObjectPool.Instance.ReleaseDelayed("IceWave",IceWave,1f);
            

            // 타겟에게 데미지 주는 코드 등 추가 가능
            ProjectileObjectPool.Instance.Release("Blizzard",gameObject);
        }
    }
}
