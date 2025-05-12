using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class ProjectileSkill : MonoBehaviour
{
    public string serialname;
    public GameObject projectilePrefab; //??沅쀯㎗??袁ⓥ봺??
    public ChangedSkillData Data; //??沅쀯㎗?곸벥 ?怨쀬뵠??
    public float fireRate; //???????獄쏆뮇沅?揶쏄쑨爰?

    public float individualFireRate;//揶쏆뮆??獄쏆뮇沅쀥첎袁㏐봄
    private float fireTimer;//??λ떄 ??볦퍢癰궰??
    //??곕뼒??

    public GameObject player;


    public SkillLevelSystem skillLevelSystem;


    private void Awake()
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
        player = PlayerController.Instance.gameObject;
        SetSkillData();
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



    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            StartCoroutine(FireWithDelay());
            fireTimer = 0;
        }

    }

    private void Fire(int count, Vector2 pivotPos, Vector2 targetPos)
    {
        GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name); //objectpool?癒?퐣 ?癒?짗??곗쨮 ?봔鈺곌퉲釉????袁ⓥ봺?諭??筌?쑴?쇾빳?

        projectile.transform.position = pivotPos;
        projectile.transform.rotation = Quaternion.identity;

        Vector2 dir = targetPos - pivotPos;
        Vector2 angleDir = Quaternion.Euler(0, 0, -(Data.angle * Data.count / 2f) + Data.angle * count) * dir;

        projectile.GetComponent<Projectile>().Init(targetPos, angleDir, Data);
    }

    private IEnumerator FireWithDelay()
    {
        for (int i = 0; i < Data.count; i++)
        {

            var currentPivotPos = player.transform.position;
            var targetTransform = player.GetComponent<PlayerController>().GetClosestEnemy();
            if (targetTransform == null) yield break;
            var currentTargetPos = targetTransform.position;

            Fire(i, currentPivotPos, currentTargetPos);
            yield return new WaitForSeconds(individualFireRate);

        }
    }


}
