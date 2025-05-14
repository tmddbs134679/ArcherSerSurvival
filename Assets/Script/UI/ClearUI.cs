using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : BaseUI
{
    public RectTransform UIBox;
    public float startPosY = 500;
    public float targetPosY = 0;
    public float duration = 1f;
    public Text text;

    private void OnEnable()
    {
        text.text = "";

        foreach(var key in AchievementManager.Instance.currentKillCnt.Keys)
        {
            text.text += "잡은 " + key + " : " + AchievementManager.Instance.currentKillCnt[key].ToString() + "마리\n";
        }
        StartCoroutine(Emergence());

    }

    public IEnumerator Emergence()
    {
        float timer = 0f;

        Vector2 startPos = new Vector2(0, startPosY);
        Vector2 endPos = new Vector2(0, targetPosY);

        UIBox.anchoredPosition = startPos;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / duration);

            t = Mathf.SmoothStep(0f, 1f, t);

            UIBox.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        UIBox.anchoredPosition = endPos;
    }

    public void LobbyBtn()
    {
        GameManager.Instance.isStartLoading = true;
        gameObject.SetActive(false);
        UIManager.Instance.FadeInUI("Loading");

    }
}
