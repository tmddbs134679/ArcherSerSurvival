using UnityEngine;

public class RecoveryItem :  UsableITem
{
    [SerializeField]
    private float health = 10;       //체력 회복 수치

    protected override void Use(GameObject target)
    {
        PlayerStat player = target.GetComponent<PlayerStat>();

        if (player != null)
        {
            player.Healed(health);
        }

       base.Use(target);
    }
}
