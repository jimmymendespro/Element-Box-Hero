using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int healthPoints = 100;

    UIUpdater uIUpdater;

    bool hasFallen = false;

    void Start()
    {
        uIUpdater = GameObject.FindObjectOfType<UIUpdater>();
        uIUpdater.InitializeUI(healthPoints, 100);
    }

    void Update() 
    {
        if(transform.position.y < -5 && !hasFallen)
        {
            hasFallen = true;
            NoHealthPoints();
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if(healthPoints <= 0)
        {
            uIUpdater.UpdatePlayerHealthUI(0);
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAction>().enabled = false;
            Invoke("NoHealthPoints", 1);
        }
        else 
        {
            uIUpdater.UpdatePlayerHealthUI(healthPoints);
        }
    }

    public void HealthRegeneration(int lifePoint = 1)
    {
        if(healthPoints + lifePoint > 100)
        {
            healthPoints = 100;
        }
        else {
            healthPoints += lifePoint;
        }
        uIUpdater.UpdatePlayerHealthUI(healthPoints);
    }

    void NoHealthPoints()
    {
        GameObject.FindObjectOfType<UIGameOver>().DisplayGameOver();
    }

}