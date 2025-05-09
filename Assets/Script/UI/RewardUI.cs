using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : BaseUI
{
    private void Awake()
    {
        //Init_Active();
    }

    [SerializeField] private Button[] rewardButtons; // 인스펙터에 버튼 3개 연결

    private void Start()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i; // 클로저 문제 방지
            rewardButtons[i].onClick.AddListener(() => SelectButton(index));
        }
    }

    public void SelectButton(int index)
    {
        Debug.Log(index);

        UIManager.Instance.HideUI(gameObject.name);
        //gameObject.SetActive(false);
    }

}
