using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField]
    private FadeEffect fadeImage;
    [SerializeField]
    private Text gameOverText;
    private void OnEnable()
    {
        fadeImage.FadeOut(Init);
    }

    private void Init()
    {
        gameOverText.gameObject.SetActive(true);
    }
}
