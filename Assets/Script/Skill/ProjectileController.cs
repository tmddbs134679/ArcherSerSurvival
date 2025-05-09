using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController2 : MonoBehaviour
{
    private ProjectileData data;//비어있는 투사체의 데이터
    private Vector2 direction;//비어있는 공격방향
    private Vector2  angleDirection;//비어있는 선처리 공격방향
    private Rigidbody2D rb;//프리팹한 투사체의 리짓바디
    public event Action<GameObject> OnClick;

    public void Init(Vector2 dir,Vector2 angleDir, ProjectileData _data)
    {
        direction = dir.normalized;
        angleDirection=angleDir.normalized;
        data = _data;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = data.color;
        Invoke("WrappingInvoke", data.duration);
    }

    private void FixedUpdate()//물리처리
    {
       // StartCoroutine(AngleDirDelay());
        rb.velocity = angleDirection * data.speed;
        transform.Rotate(Vector3.forward, data.rotateSpeed * Time.fixedDeltaTime); //프리팹 자체 회전
    }

    void OnTriggerEnter2D(Collider2D collision)//충돌했을 시
    {
        CancelInvoke("WrappingInvoke");
        OnClick?.Invoke(gameObject);//skillShooter의 ReturnToPool() 메서드를 호출
        collision.GetComponent<EnemyStateMachine>().Health.DealDamage(data.damage);
    }

    void WrappingInvoke()//Invoke(name,time) 지연 호출을 쓰려면 함수 이름으로 넣어줘야 하므로 래핑
    {
        OnClick?.Invoke(gameObject);
    }
private IEnumerator AngleDirDelay()
{
    yield return new WaitForSeconds(data.angleDelay);
    angleDirection=direction;
}
}
