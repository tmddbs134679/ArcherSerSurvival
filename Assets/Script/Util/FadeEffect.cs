using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeEffect : MonoBehaviour
{
    //??瑜곷턄????節뗪땁?띠럾? ??硫명뀬?????筌뤾쑵????겶???? 嶺뚮∥????? ?繹먮굞夷??琉우뿰 ?筌뤾쑵??
    private UnityEvent onFadeEvent = new UnityEvent();

    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;             //??瑜곷턄????濡ル츎 ??蹂?뜟
    private Image fadeImage;            //??瑜곷턄????節뗪땁???????濡ル츎 ?롪틵??? ?꾩룆?繹?????嶺뚯솘?

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

        //??瑜곷턄????節뗪땁?띠럾? ??硫몃룎嶺????繹?????덈뺄
        onFadeEvent.Invoke();
        onFadeEvent.RemoveListener(action);
    }
}
