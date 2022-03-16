using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivablePlatform : StandardPlatform
{
    protected void Awake() 
    {
        isActive = false;
    }

    public void ActivatePlatform(bool state)
    {
        isActive = state;
    }

}