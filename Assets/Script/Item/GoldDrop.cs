using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop :UsableITem
{
    [SerializeField] private int minGoldAmount = 1;
    [SerializeField] private int maxGoldAmount = 5;
    [SerializeField] private float moveSpeed = 10f;
    SFXControl playSFX;
    private Coroutine attractionCoroutine = null;
    private Transform playerTarget;

    private void Start()
    {
        playSFX = GetComponent<SFXControl>();
    }
    protected override void Use(GameObject target)
    {
        int gold = Random.Range(minGoldAmount, maxGoldAmount);
        PlayerResource player = target.GetComponent<PlayerResource>();
        if (attractionCoroutine != null)
        {
            StopCoroutine(attractionCoroutine);
        }
        if (player != null)
        {
            player.GetGold(gold);
        }

        if(playSFX != null)
        {
            playSFX.PlaySoundEffect();
        }
        ItemPool.Instance.ReturnObject(this.gameObject, "GoldDrop");
    }

    public void StartAttraction(Transform target)
    {
        if (attractionCoroutine != null)
        {
            StopCoroutine(attractionCoroutine);
        }
        playerTarget = target;
        attractionCoroutine = StartCoroutine(MoveTowardsPlayerCoroutine());
    }

    IEnumerator MoveTowardsPlayerCoroutine()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
