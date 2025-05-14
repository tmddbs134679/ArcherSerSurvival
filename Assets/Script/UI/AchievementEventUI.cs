using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementEventUI : BaseUI
{
    public RectTransform UIBox;
    public float startPosX = 1000;
    public float targetPosX = 0;
    public float duration = 1f;
    public Text text;

    private void OnEnable()
    {

        text.text = AchievementManager.Instance.achievementEvent[AchievementManager.Instance.currentKey];



        StartCoroutine(Emergence());

        Invoke("DelayOff", 2f);
    }

    public IEnumerator Emergence()
    {
        float timer = 0f;

        Vector2 startPos = new Vector2(startPosX, 0);
        Vector2 endPos = new Vector2(targetPosX, 0);

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

    public void DelayOff()
    {
        gameObject.SetActive(false);
    }
}
