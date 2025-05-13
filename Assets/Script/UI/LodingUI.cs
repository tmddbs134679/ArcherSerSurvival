using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LodingUI : BaseUI
{
    bool prevFadeFlag = false;
    public bool isLoading = true;
    public bool isStartLoading = false;
    bool endLoading = false;
    private void OnEnable()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }
    void Update()
    {

        if (GameManager.Instance.isStartLoading == true)
        {

            if (image.color.a >= 0.99)
            {
                //isLoading = false;
                PlayerController.Instance.transform.position = new Vector3(0, 0, 0);
                GameManager.Instance.isStartLoading = false;
                GameManager.Instance.NextSceneLoad();
            }

        }


    }
}
