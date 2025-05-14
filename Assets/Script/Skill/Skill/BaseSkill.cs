using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseSkill : MonoBehaviour
{
     public string serialname;
    public GameObject projectilePrefab;//투사체 프리팹
    protected ChangedSkillData Data;//투사체의 데이터
    public float fireRate;//한 사이클 발사 간격

    public float individualFireRate;//개별 발사간격
    protected float fireTimer;//단순 시간변수
     //파티클
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