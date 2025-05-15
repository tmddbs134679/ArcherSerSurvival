using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    [Header("UI ON OFF ???")]
    [SerializeField]
    private bool startActive = false;
    [Header("FadeIn Speed")]
    public float fadeDuration = 0.5f;
    public Image image;
    public bool fadeFlag = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
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


    public void BaseFadeInCoroutine()
    {
        StartCoroutine(BaseFadeIn());
    }
    public IEnumerator BaseFadeIn()
    {
        Color targetColor = image.color;
        float targetAlpha = targetColor.a;

        float timer = 0f;
        fadeFlag = true;

        image.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0f);

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / fadeDuration);

            float alpha = Mathf.Lerp(0f, targetAlpha, t);
            image.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);

            yield return null;
        }

        image.color = new Color(targetColor.r, targetColor.g, targetColor.b, targetAlpha);
        fadeFlag = false;
    }


    public void BaseFadeOutCoroutine()
    {
        StartCoroutine(BaseFadeOut());
    }


    public IEnumerator BaseFadeOut()
    {
        Color color = image.color;
        float timer = 0f;
        fadeFlag = true;

        image.color = new Color(color.r, color.g, color.b, color.a);

        while (timer < fadeDuration)
        {
            
            timer += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(1f - (timer / fadeDuration)); // ????1?????0????????
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, 0f); // ?????밸븶???????
        fadeFlag = false;
        gameObject.SetActive(false); // UI ??????μ떜媛?걫??????轅붽틓????ш퀚???
    }
}
