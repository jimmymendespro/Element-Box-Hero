using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    void Awake() 
    {
        int musicPlayerCount = FindObjectsOfType<MusicPlayer>().Length;
        if(musicPlayerCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}