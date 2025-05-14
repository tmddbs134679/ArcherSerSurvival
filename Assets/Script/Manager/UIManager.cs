using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class UIManager : Singleton<UIManager>
{
    // UI ?????????????????ъ몥????????롮쾸?椰??????????ㅳ늾????????醫딇떍???轝꿸섣??????젩????遺얘턁?????????????꿔꺂????
    private Dictionary<string, GameObject> uiElements = new Dictionary<string, GameObject>();

    [SerializeField]
    public GameObject[] uiObjects;
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

        //Linq???????????源낅???????븐뼐?곭춯?竊???????????????????
        uiObjects = objects
            .Where(obj => obj != null && obj.CompareTag("UI"))
            .ToArray();

        //?????嚥???癲?????욱룕??癒λ돗??????濚밸Ŧ援?? ?????獄쏅챶留덌┼???????
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
            uiElements[uiName].GetComponent<BaseUI>().BaseFadeInCoroutine();
        }
    }

    public void FadeOutUI(string uiName)
    {
        if (uiElements.ContainsKey(uiName))
        {
            uiElements[uiName].GetComponent<BaseUI>().BaseFadeOutCoroutine();
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
