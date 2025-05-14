using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseSkill : MonoBehaviour
{
     public string serialname;
    public GameObject projectilePrefab;//??沅쀯㎗??袁ⓥ봺??
    protected ChangedSkillData Data;//??沅쀯㎗?곸벥 ?怨쀬뵠??
    public float fireRate;//???????獄쏆뮇沅?揶쏄쑨爰?

    public float individualFireRate;//揶쏆뮆??獄쏆뮇沅쀥첎袁㏐봄
    protected float fireTimer;//??λ떄 ??볦퍢癰궰??
     //??곕뼒??
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