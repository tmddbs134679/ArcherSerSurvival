using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    private ProjectileData data;//비어있는 투사체의 데이터
    private Vector2 direction;//비어있는 공격방향
    private Rigidbody2D rb;//프리팹한 투사체의 리짓바디

    public void Init(Vector2 dir, ProjectileData _data) {
        direction = dir.normalized;
        data = _data;
        rb = GetComponent<Rigidbody2D>();

        GetComponent<SpriteRenderer>().color = data.color;
        Destroy(gameObject, data.duration);
    }

    private void FixedUpdate()//물리처리
    {
        rb.velocity = direction * data.speed;
        transform.Rotate(Vector3.forward, data.rotateSpeed * Time.fixedDeltaTime);
    }
}
