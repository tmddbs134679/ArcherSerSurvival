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

    [SerializeField] private Button[] rewardButtons; // �ν����Ϳ� ��ư 3�� ����

    private void Start()
    {
        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i; // Ŭ���� ���� ����
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
