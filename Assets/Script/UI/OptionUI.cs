using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : BaseUI
{
    public RectTransform optionBox;
    public float startPosY = 500;
    public float targetPosY = 0;
    public float duration = 1f;



    private void OnEnable()
    {
        StartCoroutine(Emergence());
    }

    private void OnDisable()
    {
        GameManager.Instance.isOption = false;
        Time.timeScale = 1.0f;
    }
    public IEnumerator Emergence()
    {
        float timer = 0f;

        Vector2 startPos = new Vector2(0, startPosY);
        Vector2 endPos = new Vector2(0, targetPosY);

        optionBox.anchoredPosition = startPos;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / duration);

            t = Mathf.SmoothStep(0f, 1f, t);

            optionBox.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        optionBox.anchoredPosition = endPos;
    }


    public void Continue()
    {
        GameManager.Instance.isOption = false;
        UIManager.Instance.HideUI("Option");
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        
    }

    public void GoLobby()
    {
        GameManager.Instance.LoadSceneLobby();
    }
}
