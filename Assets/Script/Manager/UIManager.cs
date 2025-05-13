using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class UIManager : Singleton<UIManager>
{
    // UI ???듬땹??釉띾콦???깅굵 ??㉱?洹먮뿫留??筌먦끇????용뉴
    private Dictionary<string, GameObject> uiElements = new Dictionary<string, GameObject>();

    [SerializeField]
    private GameObject[] uiObjects;
    public string uiName;

    private GameObject LodingObject;

    GameObject[] objects;




    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LodingObject = GameManager.Instance.lodingObject;
        GetUI();
    }

    private void GetUI()
    {
        GameObject parent = GameObject.Find("Canvas");
        if (parent != null)
        {
            objects = new GameObject[parent.transform.childCount];

            for (int i = 0; i < parent.transform.childCount; i++)
            {
                objects[i] = parent.transform.GetChild(i).gameObject;
            }

        }

        //Linq?????怨댄맋 濾곌쑬梨???釉띯뵛
        uiObjects = objects
            .Where(obj => obj != null && obj.CompareTag("UI"))
            .ToArray();

        //?繹먮굝裕??怨룸? ?꾩룄???
        //GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UI");
        foreach (var uiObject in uiObjects)
        {
            uiElements[uiObject.name] = uiObject;
            uiObject.GetComponent<BaseUI>().Init_Active();
        }

        if (LodingObject != null)
        {
            uiElements[LodingObject.name] = LodingObject;
        }
    }

    public void ShowUI(string uiName)
    {
        if (uiElements.ContainsKey(uiName))
        {
            uiElements[uiName].SetActive(true);
        }
    }

    public void HideUI(string uiName)
    {
        if (uiElements.ContainsKey(uiName))
        {
            uiElements[uiName].SetActive(false);
        }
    }

    public void FadeInUI(string uiName)
    {
        if (uiElements.ContainsKey(uiName))
        {
            uiElements[uiName].SetActive(true);
            uiElements[uiName].GetComponent<BaseUI>().BaseFadeIn();
        }
    }

    public void FadeOutUI(string uiName)
    {
        if (uiElements.ContainsKey(uiName))
        {
            uiElements[uiName].GetComponent<BaseUI>().BaseFadeOut(uiName);
        }
    }

    public void ToggleUI(string uiName)
    {
        if (uiElements.ContainsKey(uiName))
        {
            uiElements[uiName].SetActive(!uiElements[uiName].activeSelf);
        }
    }
}
