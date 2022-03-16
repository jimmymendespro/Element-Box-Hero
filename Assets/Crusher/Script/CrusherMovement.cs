using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherMovement : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 1f;
    [SerializeField] float phase = 0f;

    [SerializeField] ParticleSystem playerCrushed;

    Vector3 startingPosition;
    float movementFactor;

    bool isCrushing = true;

    AudioSource audioSource;

    void Start()
    {
        startingPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // 6.28.... (2pi)
        float rawSinWave = Mathf.Sin(cycles * tau + phase); // from -1 to 1
        movementFactor = (rawSinWave + 1f) / 2f; // from 0 to 1

        if(movementFactor > 0.9999f)
        {
            if(audioSource.enabled)
            {
                audioSource.volume = 0.3f;
                audioSource.Play();
            }
        }

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(isCrushing)
            {
                GameObject.FindObjectOfType<PlayerHealth>().TakeDamage(999);

                if(!audioSource.enabled)
                {
                    audioSource.enabled = true;
                }
                audioSource.volume = 0.8f;
                audioSource.Play();

                Instantiate(playerCrushed, other.GetContact(0).point, Quaternion.identity);
            }
        }
    }
}
