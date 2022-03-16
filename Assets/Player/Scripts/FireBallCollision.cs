using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCollision : MonoBehaviour
{
    [SerializeField] GameObject fireBallHit;
    [SerializeField] AudioClip fireBallHitSFX;

    GameObject player;

    AudioSource enemyAudioSource;

    void Start()
    {
        player = transform.parent.gameObject;
    }

    void OnParticleCollision(GameObject other) 
    {
        GameObject hit = Instantiate(fireBallHit, other.transform.position, Quaternion.identity);
        Destroy(hit, 0.5f);

        if(other.tag == "Enemy")
        {
            PlayFireballHitSFX(other);
            if (other.transform.parent.tag == "FireRangedEnemy")
            {
                other.transform.parent.GetComponent<EnemyHealth>().RestaureHealth(20);
            }
            else
            {
                other.transform.parent.GetComponent<EnemyHealth>().DamageTaken(20);
                other.transform.parent.GetComponent<EnemyAI>().SetIsAttacked(true);
            }

        }
    }

    private void PlayFireballHitSFX(GameObject other)
    {
        enemyAudioSource = other.transform.parent.GetComponent<AudioSource>();
        enemyAudioSource.clip = fireBallHitSFX;
        enemyAudioSource.Play();
    }
}