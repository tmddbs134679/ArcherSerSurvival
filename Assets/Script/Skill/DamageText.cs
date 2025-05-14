using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI dmgtxt;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private float Speed = 1f;
    private float lifeTime = 1f;

    private void Awake()
    {
     
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(int damage, Vector3 position)
    {
        transform.position = position;

        dmgtxt.text = $"-{damage}";
 
        StartCoroutine(TextAnim());
    }

    private IEnumerator TextAnim()
    {
        float elapsed = 0f;

        while (elapsed < lifeTime)
        {
            float t = elapsed / lifeTime;

            
            transform.position += Vector3.up * Speed * Time.deltaTime;

            
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;

   
        DamagePool.Instance.Release(this);
    }



}
