using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationOven : MonoBehaviour
{
    [SerializeField] ActivablePlatform activablePlatform;
    [SerializeField] bool isActive = false;
    [SerializeField] GameObject fireInOven;
    [SerializeField] GameObject smoke;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] AudioClip lightOffSFX;

    public bool IsActive { get { return isActive; } }

    AudioSource audioSource;
    bool isLightoffSFXPlaying = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        if(isActive)
        {
            LightFire(true);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.name == "FireBall")
        {
            LightFire(true);
            PlayFireSFX();
            isActive = true;
        }
        else if(other.name == "IceDome(Clone)")
        {
            LightFire(false);
            PlayLightOffSFX();
            isActive = false;
        }
    }

    private void LightFire(bool state)
    {
        fireInOven.SetActive(state);
        activablePlatform.ActivatePlatform(state);
        if(state)
        {
            Invoke("TurnOnSmoke", 1f);
        }
        else
        {
            Invoke("TurnOffSmoke", 1f);
        }
    }

    private void PlayFireSFX()
    {
        isLightoffSFXPlaying = false;
        audioSource.clip = fireSFX;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void PlayLightOffSFX()
    {
        if (!isLightoffSFXPlaying)
        {
            isLightoffSFXPlaying = true;
            audioSource.clip = lightOffSFX;
            audioSource.loop = false;
            audioSource.Play();
        }
    }


    void TurnOnSmoke()
    {
        smoke.SetActive(true);
        smoke.GetComponent<ParticleSystem>().Play();
    }

    void TurnOffSmoke()
    {
        smoke.GetComponent<ParticleSystem>().Stop();
    }
}