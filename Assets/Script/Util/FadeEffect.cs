using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeEffect : MonoBehaviour
{
    //???쒓낮?????影?る븕??좊읈? ??筌롫챸??????嶺뚮ㅎ?????寃뗏???? 癲ル슢?????? ?濚밸Ŧ援욃ㅇ??筌뚯슦肉??嶺뚮ㅎ???
    private UnityEvent onFadeEvent = new UnityEvent();

    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;             //???쒓낮?????嚥▲꺂痢???癰???
    private Image fadeImage;            //???쒓낮?????影?る븕???????嚥▲꺂痢??濡ろ떟??? ?袁⑸즴?濚?????癲ル슣??

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

        //???쒓낮?????影?る븕??좊읈? ??筌롫챶猷롳┼????濚??????덈틖
        onFadeEvent.Invoke();
        onFadeEvent.RemoveListener(action);
    }
}
