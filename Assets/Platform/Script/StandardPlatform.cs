using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StandardPlatform : MonoBehaviour
{
    [SerializeField] protected Vector3[] positions;
    [SerializeField] protected float platformSpeed = 5f;
    [SerializeField] protected float waitingTime = 1f;

    protected Vector3 startingPosition;
    protected float movementFactor;

    protected bool cycleComplete = true;

    protected bool isActive = true;
    
    void Start()
    {
        startingPosition = transform.position;
    }

    protected virtual void FixedUpdate()
    {
        if(cycleComplete)
        {
            if(isActive)
            {
                cycleComplete = false;
                StartPlatformMovement();
            }
        }
    }

    protected void StartPlatformMovement()
    {
        StartCoroutine("PlatformTranslation");
    }

    protected IEnumerator PlatformTranslation()
    {
        for(int i = 0 ; i < positions.Length ; i++)
        {
            yield return new WaitForSeconds(waitingTime);
            float movementPercent = 0f;
            float magnitude = Mathf.Sqrt(Mathf.Pow(positions[i].x - startingPosition.x, 2) + Mathf.Pow(positions[i].y - startingPosition.y, 2) + Mathf.Pow(positions[i].z - startingPosition.z, 2));
            while(movementPercent < 1)
            {
                if(isActive)
                {
                    movementPercent += Time.deltaTime * platformSpeed / magnitude;
                    transform.position = Vector3.Lerp(startingPosition, positions[i], movementPercent);
                }
                yield return new WaitForEndOfFrame();
            }
            startingPosition = positions[i];
        }
        cycleComplete = true;
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        if(FindObjectOfType<PlayerUtilities>().IsPlayerTag(other.gameObject.tag))
        {
            other.transform.parent = transform;
        }
    }

    protected virtual void OnTriggerExit(Collider other) 
    {
        if(FindObjectOfType<PlayerUtilities>().IsPlayerTag(other.gameObject.tag) && other.transform.parent == transform )
        {
            other.transform.parent = null;
        }
    }

}