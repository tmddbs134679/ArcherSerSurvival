using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableITem : MonoBehaviour
{
   [SerializeField] protected LayerMask playerLayer;
   protected virtual void Use(GameObject target)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            Use(collision.gameObject);
        }
    }
}
