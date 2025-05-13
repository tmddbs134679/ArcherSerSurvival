using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LodingUI : BaseUI
{
    bool prevFadeFlag = false;

    public bool isLoding = false;
    void Update()
    {
        if (prevFadeFlag && !fadeFlag)
        {
            if (isLoding == true)
            {
                isLoding = false;
                GameManager.Instance.NextSceneLoad();
            }
        }

        prevFadeFlag = fadeFlag;
    }
}
