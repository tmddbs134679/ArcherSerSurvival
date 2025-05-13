using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXControl : MonoBehaviour
{
    [SerializeField] private AudioClip dodge;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip die;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogWarning("Player Audio Source Not Found");
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
    public void OnAttackAxe()
    {

    }
}
