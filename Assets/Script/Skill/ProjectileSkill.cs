using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class ProjectileSkill : MonoBehaviour
{
    public string serialname;
    public GameObject projectilePrefab; //??雅?굞??????ш끽諭욥걡??
    public ChangedSkillData Data; //??雅?굞?????⑤챶爰????Β????
    public float fireRate; //????????袁⑸즵獒뺣뎾苡???좊즲??좊뙀?

    public float individualFireRate;//??좊즵獒???袁⑸즵獒뺣뎾苡?關苡며넭怨λ짃??
    private float fireTimer;//??縕????癰???怨뚮뼚???
    //???⑤베???

    public GameObject player;


    public SkillLevelSystem skillLevelSystem;


    private void Awake()
    {

    }

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
            player = PlayerController.Instance.gameObject;
        else
            player = gameObject;


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
        GameObject projectile = ProjectileObjectPool.Instance.Get(projectilePrefab.name); //objectpool????????筌???⑥????딅텑??釉뚰?轅대눀?????ш끽諭욥걡??????癲?????쒕춣?

        projectile.transform.position = pivotPos;
        projectile.transform.rotation = Quaternion.identity;

        Vector2 dir = targetPos - pivotPos;
        Vector2 angleDir = Quaternion.Euler(0, 0, -(Data.angle * Data.count / 2f) + Data.angle * count) * dir;

        projectile.GetComponent<Projectile>().Init(gameObject.transform.root.gameObject, targetPos, angleDir, Data);
   
    }

    private IEnumerator FireWithDelay()
    {
        for (int i = 0; i < Data.count; i++)
        {

            var currentPivotPos = player.transform.position;
            Transform targetTransform;
            
            if(player.GetComponent<PlayerTargeting>() != null)
            {
                targetTransform = player.GetComponent<PlayerTargeting>().GetClosestEnemy();
            }
            else
            {
                targetTransform = GetComponent<EnemyStateMachine>().Player.transform;
            }

                

            if (targetTransform == null) yield break;
            var currentTargetPos = targetTransform.position;

            Fire(i, currentPivotPos, currentTargetPos);
            yield return new WaitForSeconds(individualFireRate);

        }
    }


}
