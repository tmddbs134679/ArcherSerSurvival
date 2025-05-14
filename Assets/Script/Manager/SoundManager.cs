using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioClip TitleBGM;
    [SerializeField] private AudioClip DungeonBGM;
    [SerializeField] private AudioClip BossBGM;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void StopMusic()
    {
        if (audioSource != null) 
        {
            audioSource.Stop();
        }
    }

    public void PlayTitleBGM()
    {
        PlayBGM(TitleBGM);
    } 
    
    public void PlayDungeonBGM()
    {
        PlayBGM(DungeonBGM);
    } 
    
    public void PlayBossBGM()
    {
        PlayBGM(TitleBGM);
    }

    private void PlayBGM(AudioClip clip)
    {
        Debug.Log(audioSource.clip);
        if(audioSource.clip == clip)
        {
            return;
        }

        if(audioSource != null) 
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
    
}
