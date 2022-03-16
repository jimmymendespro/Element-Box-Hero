using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PositionningEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector3[] positions;

    NavMeshAgent navMeshAgent;

    int currentPosition = 0;

    bool alreadyPositionned = false;

    void Start() 
    {
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if(!alreadyPositionned)
        {
            alreadyPositionned = true;
            EnemyPositionning(positions[0]);
            enemy.GetComponent<Animator>().SetBool("Positionned", true);
        }
    }

    void EnemyPositionning(Vector3 nextPosition)
    {
        navMeshAgent.SetDestination(nextPosition);
        StartCoroutine("CheckIfPositionned", nextPosition);
    }

    IEnumerator CheckIfPositionned(Vector3 nextPosition)
    {
        while(Vector3.Distance(enemy.transform.position, nextPosition) > navMeshAgent.stoppingDistance)
        {
            yield return new WaitForEndOfFrame();
        }
        if(currentPosition < positions.Length - 1)
        {
            currentPosition++;
            EnemyPositionning(positions[currentPosition]);
        }
    }

}