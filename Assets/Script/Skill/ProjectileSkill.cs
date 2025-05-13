using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class ProjectileSkill : MonoBehaviour
{
    public string serialname;
    public GameObject projectilePrefab;//투사체 프리팹
    ChangedSkillData Data;//투사체의 데이터
    public float fireRate;//한 사이클 발사 간격

    public float individualFireRate;//개별 발사간격
    private float fireTimer;//단순 시간변수
     //파티클

    public GameObject SkillOwner;


    public SkillLevelSystem skillLevelSystem;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Init();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Init();
    }

private void Init()
{
    if (gameObject.GetComponentInParent<PlayerController>() != null)
    {
        SkillOwner = PlayerController.Instance.gameObject;
    }
    else
    {
        SkillOwner = gameObject;
        // 몬스터는 스킬 데이터를 따로 설정하지 않음
    }
            SetSkillData(); // 플레이어일 경우만 스킬 데이터 설정
}


    public void SetSkillData()
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



    protected virtual void Update()
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
        GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name); //objectpool????????筌???⑥????딅텑??釉뚰?轅대눀?????ш끽諭욥걡??????癲?????쒕춣?

        projectile.transform.position = SkillOwner.transform.position;
        projectile.transform.rotation = Quaternion.identity;

        Vector2 dir = Target.transform.position - SkillOwner.transform.position;
        Vector2 angleDir = Quaternion.Euler(0, 0, -(Data.angle * Data.count / 2f) + Data.angle * count) * dir; //

        projectile.GetComponent<Projectile>().Init(SkillOwner,Target, angleDir, Data);
   
    }

    private IEnumerator FireWithDelay()
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
