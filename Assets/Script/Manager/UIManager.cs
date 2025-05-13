using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class UIManager : Singleton<UIManager>
{
    // UI ???????????⑥ル럯????嚥싲갭큔????????곸궔?????녾컯???觀?쎾＄???轅붽틓??????????筌뤾퍓?
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
        //Linq?????????닿튃癲??饔낅챷維??????????⑥ロ꺘??
        uiObjects = objects
            .Where(obj => obj != null && obj.CompareTag("UI"))
            .ToArray();
        //??關?쒎첎?嫄??怨룻돲嶺??????깅즽? ????썹땟戮녹????
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
            //uiElements[uiName].GetComponent<BaseUI>().BaseFadeOutCoroutine();
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