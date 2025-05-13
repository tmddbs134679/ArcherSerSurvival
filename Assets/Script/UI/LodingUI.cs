using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LodingUI : BaseUI
{
    bool prevFadeFlag = false;
    public bool isLoading = true;


    void Update()
    {

        if (isLoading == true)
        {
            if (image.color.a >= 0.99)
            {
                //isLoading = false;
                PlayerController.Instance.transform.position = new Vector3(0, 0, 0);
                GameManager.Instance.NextSceneLoad();
            }
        }


    }
}
