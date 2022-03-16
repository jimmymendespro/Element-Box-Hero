using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableElevator : ActivablePlatform
{
    ActivablePlatform thisPlatform;
    RaycastHit underPlatformCheck;

    void Start() 
    {
        startingPosition = transform.position;
        thisPlatform = GetComponent<ActivablePlatform>();
    }

    void OnCollisionEnter(Collision other) 
    {
        if(FindObjectOfType<PlayerUtilities>().IsPlayerTag(other.gameObject.tag))
        {
            other.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(FindObjectOfType<PlayerUtilities>().IsPlayerTag(other.gameObject.tag) && other.transform.parent == transform )
        {
            other.transform.parent = null;
        }
    }

    protected override void OnTriggerEnter(Collider other) 
    {
        if(FindObjectOfType<PlayerUtilities>().IsPlayerTag(other.gameObject.tag))
        {
            thisPlatform.ActivatePlatform(false);
        }
    }

    protected override void OnTriggerExit(Collider other) 
    {
        if(FindObjectOfType<PlayerUtilities>().IsPlayerTag(other.gameObject.tag))
        {
            thisPlatform.ActivatePlatform(true);
        }
    }

}