using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;



public class ExplosionSkill : BaseSkill
{
    
    protected override void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Init();
    }

    protected override void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Init();
    }

    protected override void Init()
{
    if (gameObject.GetComponentInParent<PlayerController>() != null)
    {
        SkillOwner = PlayerController.Instance.gameObject;
    }
    else
    {
        SkillOwner = gameObject;
        
    }
            SetSkillData();//기본 스탯 설정
}


    public override void SetSkillData()
    {
        Data = new ChangedSkillData();

        skillLevelSystem = GameManager.Instance.skillLevelSystem;
        Data.speed = skillLevelSystem.changedSkillData[serialname].speed;
        Data.damage = skillLevelSystem.changedSkillData[serialname].damage;
        Data.duration = skillLevelSystem.changedSkillData[serialname].duration;
        Data.color = skillLevelSystem.changedSkillData[serialname].color;
        Data.impactEffect = skillLevelSystem.changedSkillData[serialname].impactEffect;
        Data.rotateSpeed = skillLevelSystem.changedSkillData[serialname].rotateSpeed;
        Data.count = skillLevelSystem.changedSkillData[serialname].count;
        Data.angle = skillLevelSystem.changedSkillData[serialname].angle;
        Data.hormingStartDelay = skillLevelSystem.changedSkillData[serialname].hormingStartDelay;
        Data.hormingTurnDelay = skillLevelSystem.changedSkillData[serialname].hormingTurnDelay;
    }


    protected override void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            StartCoroutine(FireWithDelay());
            fireTimer = 0;
        }

    }

    public void Fire(int count,GameObject SkillOwner,GameObject Target)
    {
    //  랜덤 위치 오프셋
    float radius = 5f; // 폭발 반경
    Vector2 randomOffset = Random.insideUnitCircle * radius;

    //  최종 위치 계산
    Vector2 spawnPosition = (Vector2)SkillOwner.transform.position + randomOffset;

    //  오브젝트 가져오기
    GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name);
    projectile.transform.position = spawnPosition;
        projectile.transform.rotation = Quaternion.identity;
        projectile.GetComponent<Explosion>().Init(SkillOwner,Target,Data);
    }

    protected IEnumerator FireWithDelay()
    {
        for (int i = 0; i < Data.count; i++)
        {
            GameObject TargetTemp=null;
            
              if (SkillOwner.layer == LayerMask.NameToLayer("Player")) //SkillOwner가 플레이어일시 타겟 탐색
              {
                  TargetTemp = SkillOwner.GetComponent<PlayerTargeting>().GetClosestEnemy()?.gameObject;
              }
            else
            {
                TargetTemp = GetComponent<EnemyStateMachine>().Player;//아닐시 몬스터
            }
            if (TargetTemp == null) yield break;
            Fire(i,SkillOwner,TargetTemp);
            yield return new WaitForSeconds(individualFireRate);

        }
    }



}
