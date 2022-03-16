using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDomeCollision : MonoBehaviour
{
    [SerializeField] AudioClip iceDomeHitSFX;

    AudioSource enemyAudioSource;

    void OnParticleCollision(GameObject other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<EnemyState>().FreezeEnemy();
            PlayIceDomeHitSFX(other);
        }
    }

    private void PlayIceDomeHitSFX(GameObject other)
    {
        enemyAudioSource = other.transform.parent.GetComponent<AudioSource>();
        enemyAudioSource.clip = iceDomeHitSFX;
        enemyAudioSource.Play();
    }
}
