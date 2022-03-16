using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperPiece : MonoBehaviour
{
    ParticleSystem playerCrushed;

    float initialTime;

    void OnEnable() 
    {
        initialTime = Time.time;
        Invoke("DestroyDropperPiece", 10);
    }

    void OnCollisionEnter(Collision other) 
    {
        if(Time.time < initialTime + 3)
        {
            if(other.gameObject.tag == "Player")
            {
                GameObject.FindObjectOfType<PlayerHealth>().TakeDamage(20);
            }
        }
    }

    void DestroyDropperPiece()
    {
        Destroy(this.gameObject);
    }
}