using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour
{
    [SerializeField] Material naturalMaterial;
    [SerializeField] Material frozenMaterial;
    [SerializeField] float freezingTime = 5f;

    bool isFrozen = false;
    public bool IsFrozen { get { return isFrozen; } }

    public void FreezeEnemy()
    {
        if(!isFrozen)
        {
            isFrozen = true;
            GetComponent<Animator>().SetBool("isFrozen", true);
            GetComponent<Animator>().Rebind(); // Permet d'interrompre l'ennemi quelquesoit son état en réinitialisant son animator.
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponentInChildren<MeshRenderer>().material = frozenMaterial;
            Invoke("Unfreeze", freezingTime);
        }
    }

    void Unfreeze()
    {
        isFrozen = false;
        GetComponent<Animator>().SetBool("isFrozen", false);
        GetComponent<Animator>().SetBool("Positionned", true); // Doit être activé pour que l'ennemi repasse dans un état idle
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponentInChildren<MeshRenderer>().material = naturalMaterial;
    }
}
