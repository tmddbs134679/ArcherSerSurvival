using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveWizard : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    NPCController controller;

    private void Start()
    {
        controller = GetComponent<NPCController>();
        if(controller != null)
        {
            controller.onDialogEndEvent.AddListener(EnterTutoral);
        }
    }

    public void EnterTutoral(GameObject player)
    {
        SceneManager.LoadScene(sceneName);
    }
}
