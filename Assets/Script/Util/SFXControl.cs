using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControl : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect()
    {
        if (audioSource != null && audioClip != null)
        {
            Debug.Log("playOneShot");
            AudioSource.PlayClipAtPoint(audioClip, PlayerController.Instance.transform.position, 1.0f);
            //audioSource.PlayClip(audioClip);
        }
    }
}

 
