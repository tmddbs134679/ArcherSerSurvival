using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXControl : MonoBehaviour
{
    [SerializeField] private AudioClip dodge;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip die;

    [SerializeField] private Dictionary<string, AudioClip> projectileSFX = new Dictionary<string, AudioClip>();
    
    [SerializeField] private AudioClip[] weaponSounds;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogWarning("Player Audio Source Not Found");
        }

        foreach(AudioClip clip in weaponSounds)
        {
            string name = clip.name;
            Debug.Log(clip);
            projectileSFX.Add(name.Substring(0, clip.name.Length - "_Sound".Length), clip);
            Debug.Log(name.Substring(0, clip.name.Length - "_Sound".Length));
        }
    }

    public void OnDodge()
    {
        audioSource.PlayOneShot(dodge);
    } 
    public void OnHit()
    {
        audioSource.PlayOneShot(hit);
    } 
    public void Ondie()
    {
        audioSource.PlayOneShot(die);
    }
    public void OnAttack(string name)
    {
        Debug.Log(name);
        AudioClip clip = projectileSFX[name];
        Debug.Log(clip);
        if (clip == null)
        {
            return;
        }
        audioSource.PlayOneShot(clip);
    }
}
