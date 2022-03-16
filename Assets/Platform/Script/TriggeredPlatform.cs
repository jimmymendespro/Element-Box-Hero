using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredPlatform : ActivablePlatform
{

    protected override void OnTriggerEnter(Collider other) 
    {
        if(FindObjectOfType<PlayerUtilities>().IsPlayerTag(other.gameObject.tag))
        {
            isActive = true;
            other.transform.parent = transform;
        }
    }

}