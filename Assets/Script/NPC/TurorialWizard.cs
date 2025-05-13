using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurorialWizard : MonoBehaviour
{
    [SerializeField]
    private string tutorialSceneName;
    NPCController controller;

    private void Start()
    {
        controller = GetComponent<NPCController>();
        if(controller != null)
        {
            controller.onDialogEndEvent.AddListener(EnterTutoral);
        }
    }

    public void EnterTutoral()
    {
        SceneManager.LoadScene(tutorialSceneName);
    }
}
