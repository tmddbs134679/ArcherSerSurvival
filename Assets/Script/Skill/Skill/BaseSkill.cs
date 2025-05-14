using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseSkill : MonoBehaviour
{
     public string serialname;
    public GameObject projectilePrefab;//??亦낆?럸??熬곣뱿遊??
    protected ChangedSkillData Data;//??亦낆?럸?怨몃꺄 ??⑥щ턄??
    public float fireRate;//????????꾩룇裕뉑쾮??띠룄?①댆?

    public float individualFireRate;//?띠룇裕???꾩룇裕뉑쾮?μ쾸熬곥룓遊?
    protected float fireTimer;//??貫????蹂?뜟?곌떠???
     //??怨뺣폃??
    public GameObject SkillOwner;
    public int animationName;

    public SkillLevelSystem skillLevelSystem;

    protected abstract void Start();
    protected abstract void OnDestroy();
    protected abstract void OnSceneLoaded(Scene scene, LoadSceneMode mode);
    protected abstract void Init();
    public abstract void SetSkillData();
    protected abstract void Update();

    public virtual void Execute(EnemyStateMachine enemy, Action onComplete){ }
}