using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] int damage = 25;
    [SerializeField] GameObject playerHit;
    [SerializeField] AudioClip attackSFX;
    [SerializeField] AudioClip playerHitSFX;
    GameObject player;

    AudioSource enemyAudioSource;
    AudioSource playerAudioSource;

    void Start() 
    {
        player = FindObjectOfType<PlayerHealth>().gameObject;
        enemyAudioSource = GetComponent<AudioSource>();
        playerAudioSource = player.GetComponent<AudioSource>();
    }

    public void AttackHitEvent()
    {
        if(player != null)
        {
            PlayAttackSFX();
            float distancePlayerEnnemi = Vector3.Distance(transform.position, player.transform.position);

            if (distancePlayerEnnemi < GetComponent<NavMeshAgent>().stoppingDistance) // Uniquement si le joueur est touché 
            {
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
                GameObject hit = Instantiate(playerHit, player.transform.position, Quaternion.identity);
                PlayHitSFX();
                // TODO CORRIGER LE RECUL
                player.GetComponent<Rigidbody>().AddForce(transform.forward * 800);
            }
        }
    }

    private void PlayAttackSFX()
    {
        enemyAudioSource.clip = attackSFX;
        enemyAudioSource.Play();
    }

    void PlayHitSFX()
    {
        playerAudioSource.clip = playerHitSFX;
        playerAudioSource.Play();
    }
}