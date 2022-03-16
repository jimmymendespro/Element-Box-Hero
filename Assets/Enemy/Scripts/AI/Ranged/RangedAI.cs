using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedAI : EnemyAI
{
    [SerializeField] float viewAngle = 4f;
    [SerializeField] protected float fireRange = 50f;
    [SerializeField] protected float meleeRange = 15f;
    [SerializeField] ParticleSystem projectile;
    [SerializeField] AudioClip projectileSFX;

    Vector3 playerDirection;
    float angleBewteenForwardAndPlayer;

    protected bool projectileFired = false;

    protected override void Update()
    {
        // playerDirection est le vecteur EP (Enemy->Player)
        playerDirection = player.position - transform.position;
        distanceToPlayer = Vector3.Magnitude(playerDirection);

        angleBewteenForwardAndPlayer = Vector3.Angle(Vector3.forward, this.transform.TransformVector(playerDirection));

        if ((hasSeenPlayer || isAttacked) && !enemyState.IsFrozen)
        {
            LookAtPlayer();
            EngagePlayer();
        }
        else if (distanceToPlayer <= range && angleBewteenForwardAndPlayer < viewAngle)
        {
            hasSeenPlayer = true;
        }
    }

    protected override void EngagePlayer()
    {
        if(base.distanceToPlayer > fireRange)
        {
            GetCloserPlayer();
        }
        else
        {
            if(base.distanceToPlayer > meleeRange)
            {
                RangedAttack();
            }
            else 
            {
                if(distanceToPlayer > navMeshAgent.stoppingDistance)
                {
                    ChasePlayer();
                }
                else
                {
                    MeleeAttack();
                }
            }
        }
    }

    protected void GetCloserPlayer()
    {
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(base.player.position);
    }

    protected void RangedAttack()
    {
        navMeshAgent.isStopped = true;
        if (base.player.transform.position.y - transform.position.y < 1 && !projectileFired)
        {
            projectileFired = true;
            StartCoroutine("EmitProjectile");
            PlayProjectileSFX();
        }
    }

    IEnumerator EmitProjectile()
    {
        projectile.Emit(1);
        yield return new WaitForSeconds(1f);
        projectileFired = false;
    }

    private void PlayProjectileSFX()
    {
        audioSource.clip = projectileSFX;
        audioSource.volume = 0.2f;
        audioSource.spatialBlend = 0f;
        audioSource.Play();
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, fireRange);
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }

}