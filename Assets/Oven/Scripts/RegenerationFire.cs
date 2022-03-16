using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationFire : MonoBehaviour
{
    [SerializeField] float regenerationFactor = 0.5f;

    AudioSource audioSource;

    bool isAudioPlaying = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnParticleCollision(GameObject other) 
    {
        if(other.tag == "PlayerFireMode")
        {
            StartCoroutine("Regen");
            if(!isAudioPlaying)
            {
                isAudioPlaying = true;
                audioSource.Play();
                Invoke("SetIsAudioPlayigToFalse", 5);
            }
        }
    }

    IEnumerator Regen()
    {
        FindObjectOfType<PlayerHealth>().HealthRegeneration();
        FindObjectOfType<Reload>().FireRegeneration();
        yield return new WaitForSeconds(regenerationFactor);
    }

    void SetIsAudioPlayigToFalse()
    {
        isAudioPlaying = false;
    }
    
}