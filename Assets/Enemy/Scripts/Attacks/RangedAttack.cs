using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] protected int projectileDamage = 20;
    [SerializeField] protected AudioClip playerHitSFX;
    [SerializeField] protected GameObject playerHit;
    [SerializeField] protected GameObject sceneHit;

    protected PlayerHealth playerHealth;

    protected AudioSource playerAudioSource;

    private void Start() 
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerAudioSource = playerHealth.gameObject.GetComponent<AudioSource>();
    }

    protected virtual void OnParticleCollision(GameObject other) 
    {
        if(FindObjectOfType<PlayerUtilities>().IsPlayerTag(other.tag))
        {
            playerHealth.TakeDamage(projectileDamage);
            GameObject hit = Instantiate(playerHit, other.transform.position, Quaternion.identity);
            Destroy(hit, 0.5f);
            PlayHitSFX();
        }
        else
        {
            GameObject hit = Instantiate(sceneHit, other.transform.position, Quaternion.identity);
            Destroy(hit, 0.5f);
        }
    }

    void PlayHitSFX()
    {
        playerAudioSource.clip = playerHitSFX;
        playerAudioSource.Play();
    }
}
