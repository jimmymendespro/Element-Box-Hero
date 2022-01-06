using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformOscillator : MonoBehaviour
{
    [SerializeField] Vector3 endPosition;
    [SerializeField] float platformSpeed = 0.3f;

    Vector3 startingPosition;
    float movementFactor;

    bool isReadyToGo = false;
    bool isTransitionning = false;
    
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if(!isReadyToGo)
        {
            isReadyToGo = true;
            Invoke("StartPlatformMovement", 1f);
        }
    }

    void StartPlatformMovement()
    {
        isTransitionning = true;
        StartCoroutine("PlatformTranslation");
    }

    IEnumerator PlatformTranslation()
    {
        float movementPercent = 0f;
        while(movementPercent < 1)
        {
            movementPercent += Time.deltaTime * platformSpeed;
            transform.position = Vector3.Lerp(startingPosition, endPosition, movementPercent);
            yield return new WaitForEndOfFrame();
        }
        Vector3 temp = startingPosition;
        startingPosition = endPosition;
        endPosition = temp;
        isTransitionning = false;
        isReadyToGo = false;
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent.transform.parent = transform;
        }
        
    }

    void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Player" && other.transform.parent.transform.parent == transform )
        {
            other.transform.parent.transform.parent = null;
        }
    }

    
}