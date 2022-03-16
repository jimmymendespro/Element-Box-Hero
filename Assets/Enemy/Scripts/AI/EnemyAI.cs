using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] protected float range = 15f;
    [SerializeField] protected float turnSpeed = 5f;
    [SerializeField] protected bool needsPositionning = false;

    protected NavMeshAgent navMeshAgent;

    protected EnemyState enemyState;
    protected bool hasSeenPlayer = false;
    protected bool isAttacked = false;
    
    protected Transform player;
    protected float distanceToPlayer = Mathf.Infinity;

    protected AudioSource audioSource;

    void Start() 
    {
        enemyState = GetComponent<EnemyState>();
        player = FindObjectOfType<PlayerHealth>().gameObject.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        if(!needsPositionning)
        {
            GetComponent<Animator>().SetBool("Positionned", true);
        }
    }

    protected virtual void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if ((hasSeenPlayer || isAttacked) && !enemyState.IsFrozen)
        {
            LookAtPlayer();
            EngagePlayer();
        }
        else if (distanceToPlayer <= range)
        {
            hasSeenPlayer = true;
        }
        AdaptAcceleration();
    }

    protected void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    protected virtual void EngagePlayer()
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

    protected void ChasePlayer()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(player.position);
        navMeshAgent.isStopped = false;
    }

    protected virtual void MeleeAttack()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    // Permet d'adapter l'accélération pour que le mesh enemy ne collisionne pas avec le joueur
    void AdaptAcceleration()
    {
        if (navMeshAgent.hasPath)
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                navMeshAgent.acceleration = 30f;
            }
            else
            {
                navMeshAgent.acceleration = 8f;
            }
        }
    }

    public void SetIsAttacked(bool state)
    {
        isAttacked = state;
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    
}