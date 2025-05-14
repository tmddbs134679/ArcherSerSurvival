using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDrop : UsableITem
{
    SFXControl sfxControl;

    private void Start()
    {
        sfxControl =GetComponent<SFXControl>();
    }
    protected override void Use(GameObject target)
    {
        GoldDrop[] goldItems = ItemPool.Instance.GetComponentsInChildren<GoldDrop>();

        foreach (GoldDrop gold in goldItems)
        {
            Debug.Log(gold.name);
            if (gold.gameObject.activeSelf)
            {
                gold.GetComponent<GoldDrop>().StartAttraction(PlayerController.Instance.transform);
            }
        }
        if (sfxControl != null)
        {
            sfxControl.PlaySoundEffect();
        }
        ItemPool.Instance.ReturnObject(this.gameObject, "MagnetDrop");
    }
}
