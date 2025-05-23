using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class UIManager : Singleton<UIManager>
{
    // UI ??????????????????紐????????濡?씀?濾???????????노듋?????????ル뵁????饔앷옇??????????????븐뼐??????????????轅붽틓????
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

        //Linq???????????繹먮굝???????釉먮폁?怨?땡?塋???????????????????
        uiObjects = objects
            .Where(obj => obj != null && obj.CompareTag("UI"))
            .ToArray();

        //?????????????????깅짆???믊삳룛??????嚥싲갭큔??? ??????꾩룆梨띰쭕?뚢뵾???????
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
