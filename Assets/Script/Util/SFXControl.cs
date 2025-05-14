using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControl : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    public void PlaySoundEffect()
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip,transform.position, 1.0f);
        }
    }
}

 
