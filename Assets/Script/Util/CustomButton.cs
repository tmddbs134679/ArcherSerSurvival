using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    protected override void Awake()
    {
        base.Awake();
        //gameObject.GetComponent<UIManager>
        Debug.Log(gameObject);
        onClick.AddListener(() => UIManager.Instance.ToggleUI(gameObject.name));
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        // 엔터무시
        if (eventData is PointerEventData pointerEventData)
        {
            return;
        }

    }
}
