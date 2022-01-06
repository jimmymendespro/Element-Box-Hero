using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherMovement : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 1f;
    [SerializeField] float phase = 0f;

    Vector3 startingPosition;
    float movementFactor;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if(period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // 6.28.... (2pi)
        float rawSinWave = Mathf.Sin(cycles * tau + phase); // from -1 to 1    
        movementFactor = (rawSinWave + 1f) / 2f; // from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }

    void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Crushed !");
    }
}
