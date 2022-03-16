using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedFireAttack : RangedAttack
{
    [SerializeField] GameObject fireHit;
    Reload playerReload;

    void Start() 
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerReload = FindObjectOfType<Reload>();
        playerAudioSource = playerHealth.gameObject.GetComponentInParent<AudioSource>();
    }

    protected override void OnParticleCollision(GameObject other) 
    {
        switch(other.tag)
        {
            case "PlayerFireMode":
                playerReload.FireRegeneration(20);
                break;
            case "Player":
            case "PlayerIceMode":
            case "PlayerElectrikMode":
            case "PlayerWindMode":
                playerHealth.TakeDamage(base.projectileDamage);
                break;
        }
        GameObject hit = Instantiate(fireHit, other.transform.position, Quaternion.identity);
        Destroy(hit, 0.5f);
        playerAudioSource.clip = playerHitSFX;
        playerAudioSource.Play();
    }

}