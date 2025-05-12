using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class ProjectileSkill : MonoBehaviour
{
    public string serialname;
    public GameObject projectilePrefab; //??亦낆?럸??熬곣뱿遊??
    public ChangedSkillData Data; //??亦낆?럸?怨몃꺄 ??⑥щ턄??
    public float fireRate; //????????꾩룇裕뉑쾮??띠룄?①댆?

    public float individualFireRate;//?띠룇裕???꾩룇裕뉑쾮?μ쾸熬곥룓遊?
    private float fireTimer;//??貫????蹂?뜟?곌떠???
    //??怨뺣폃??

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
        GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name); //objectpool????????吏??怨쀬Ŧ ?遊붋?브퀗?꿴뇡????熬곣뱿遊?獄???嶺????얜뭄?

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
