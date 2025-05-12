using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeEffect : MonoBehaviour
{
    //페이드 효과가 끝났을 때 호출하고 싶은 메소드를 등록하여 호출
    private UnityEvent onFadeEvent = new UnityEvent();

    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;             //페이드 되는 시간
    private Image fadeImage;            //페이드 효과에 사용되는 검은 바탕 이미지

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

        //페이드 효과가 끝나면 이벤트 실행
        onFadeEvent.Invoke();
        onFadeEvent.RemoveListener(action);
    }
}
