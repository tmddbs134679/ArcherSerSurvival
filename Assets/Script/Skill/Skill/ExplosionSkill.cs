using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;


public class ExplosionSkill : BaseSkill
{
    public bool RandomCheck;
    public static event Action<GameObject,float> OnMeteorFired;
    public static event Action<GameObject,float> OnIceSpearFired;

    PlayerSFXControl sfxControl;

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
        SetSkillData();//湲곕낯 ?ㅽ꺈 ?ㅼ젙
        sfxControl = GetComponentInParent<PlayerSFXControl>();
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

    public void Fire(int count, GameObject SkillOwner, GameObject Target)
    {
        //  ?쒕뜡 ?꾩튂 ?ㅽ봽??
        float radius = 5f; // ??컻 諛섍꼍
        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * radius;

        //  理쒖쥌 ?꾩튂 怨꾩궛
        Vector2 spawnPosition = (Vector2)SkillOwner.transform.position + randomOffset;

        //  ?ㅻ툕?앺듃 媛?몄삤湲?
        GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name);
        RandomCheck=RandomBool();
        if(RandomCheck)
        {
        projectile.transform.position =Target.transform.position;//?寃??꾩튂 ??컻
        }
        else
        {
       projectile.transform.position = spawnPosition;//?쒕뜡 ?꾩튂 ??컻
        }
        projectile.transform.rotation = Quaternion.identity;
        projectile.GetComponent<Explosion>().Init(SkillOwner, Target,Data);
        if(serialname=="Ice")OnIceSpearFired?.Invoke(projectile,Data.duration);
        else if(serialname=="Meteo")OnMeteorFired?.Invoke(projectile,Data.duration);

        if (sfxControl != null)
        {
            sfxControl.OnAttack(serialname);
        }
    }

    protected IEnumerator FireWithDelay()
    {
        for (int i = 0; i < Data.count; i++)
        {
            GameObject TargetTemp = null;

            if (SkillOwner.layer == LayerMask.NameToLayer("Player")) //SkillOwner媛 ?뚮젅?댁뼱?쇱떆 ?寃??먯깋
            {
                TargetTemp = SkillOwner.GetComponent<PlayerTargeting>().GetClosestEnemy()?.gameObject;
            }
            else
            {
                TargetTemp = GetComponent<EnemyStateMachine>().Player;//?꾨땺??紐ъ뒪??
            }
            if (TargetTemp == null) yield break;
            Fire(i, SkillOwner, TargetTemp);
            yield return new WaitForSeconds(individualFireRate);

        }
    }

    public bool RandomBool()
    {
        System.Random random = new System.Random();
        return random.NextDouble() < 0.4;
    }


}
