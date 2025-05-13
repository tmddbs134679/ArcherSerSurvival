using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeEffect : MonoBehaviour
{
    //??륁뵠????ｋ궢揶쎛 ??멸텢?????紐꾪뀱??랁???? 筌롫뗄???? ?源낆쨯??뤿연 ?紐꾪뀱
    private UnityEvent onFadeEvent = new UnityEvent();

    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;             //??륁뵠????롫뮉 ??볦퍢
    private Image fadeImage;            //??륁뵠????ｋ궢???????롫뮉 野꺜?? 獄쏅?源????筌왖

    private void Start()
    {
        fadeImage = GetComponent<Image>();
    }

    public void FadeIn(UnityAction action)
    {
        StartCoroutine(Fade(action, 1, 0)); 
    } 
    public void FadeOut(UnityAction action)
    {
        StartCoroutine(Fade(action, 0, 1)); 
    }

    private IEnumerator Fade(UnityAction action, float start, float end)
    {
        if(fadeImage == null)
        {
            fadeImage = GetComponent<Image>();
        }
        onFadeEvent.AddListener(action);

        float current = 0f;
        float percent = 0f;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;

            yield return null;
        }

        //??륁뵠????ｋ궢揶쎛 ??멸돌筌???源????쎈뻬
        onFadeEvent.Invoke();
        onFadeEvent.RemoveListener(action);
    }
}
