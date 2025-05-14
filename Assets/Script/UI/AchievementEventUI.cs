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


    public Sprite[] images;
    public Image icon;
    public Dictionary<string, Sprite> enemyImages = new Dictionary<string, Sprite>();


    private void OnEnable()
    {
        enemyImages = new Dictionary<string, Sprite>();
        enemyImages.Add("Doc", images[0]);
        enemyImages.Add("Skelet", images[1]);
        //enemyImages.Add("", images[2]);

        text.text = AchievementManager.Instance.achievementEvent[AchievementManager.Instance.currentKey];

        icon.sprite = enemyImages[AchievementManager.Instance.currentKey];


        StartCoroutine(Emergence());

        Invoke("DelayOff", 3.5f);
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
