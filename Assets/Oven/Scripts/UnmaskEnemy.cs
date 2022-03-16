using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmaskEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] enemys;

    ActivationOven activationOven;
    bool isUnmasked = false;

    void Start() 
    {
        activationOven = GetComponent<ActivationOven>();
    }

    void Update() 
    {
        if(activationOven.IsActive && !isUnmasked)
        {
            Unmask();
        }
    }

    public void Unmask()
    {
        foreach(GameObject enemy in enemys)
        {
            enemy.SetActive(true);
        }
        isUnmasked = true;
    }
}
