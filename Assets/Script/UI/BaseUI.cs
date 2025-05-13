using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [Header("UI ON OFF 여부")]
    [SerializeField]
    private bool startActive = false;
    public void ExitUI()
    {
        gameObject.SetActive(false);
    }

    public void Init_Active()
    {
        
        if(startActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }
}
