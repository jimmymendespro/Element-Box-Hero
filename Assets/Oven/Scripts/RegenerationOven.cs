using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationOven : MonoBehaviour
{
    [SerializeField] GameObject fireInOven;
    [SerializeField] GameObject smokeBottomLeft;
    [SerializeField] GameObject smokeBottomRight;
    [SerializeField] GameObject smokeUpLeft;
    [SerializeField] GameObject smokeUpRight;
    [SerializeField] AudioClip fireSFX;

    AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnParticleCollision(GameObject other) 
    {
        fireInOven.SetActive(true);
        PlayFireSFX();
        Invoke("TurnOnSmoke", 1f);
        Destroy(GetComponent<BoxCollider>());
    }

    private void PlayFireSFX()
    {
        audioSource.clip = fireSFX;
        audioSource.loop = true;
        audioSource.Play();
    }

    void TurnOnSmoke()
    {
        smokeBottomLeft.SetActive(true);
        smokeBottomRight.SetActive(true);
        smokeUpLeft.SetActive(true);
        smokeUpRight.SetActive(true);
    }

    void TurnOffSmoke()
    {
        smokeBottomLeft.SetActive(false);
        smokeBottomRight.SetActive(false);
        smokeUpLeft.SetActive(false);
        smokeUpRight.SetActive(false);
    }
}
