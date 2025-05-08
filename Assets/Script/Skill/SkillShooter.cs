using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShooter : MonoBehaviour {
    public GameObject projectilePrefab; //투사체 프리팹
    public ProjectileData projectileData; //투사체의 데이터
    public float fireRate; //발사 간격
    private float fireTimer;//단순 시간변수

    private void Update() {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate) {
            Fire();
            fireTimer = 0;
        }
    }

    private void Fire() {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);//투사체 생성(프리팹,위치,회전)
        projectile.GetComponent<ProjectileController>().Init(transform.right, projectileData); //투사체에게 투사체의 데이터+공격방향 전달
    }
}
