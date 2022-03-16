using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperTrigger : MonoBehaviour
{
    [SerializeField] Dropper[] droppers;

    bool hasAlreadyBeenActivated = false;

    void OnTriggerEnter(Collider other) 
    {
        if(!hasAlreadyBeenActivated)
        {
            if(other.tag == "Player")
            {
                ShakeCamera();
                ActivateDropper();
            }
            hasAlreadyBeenActivated = true;
        }
    }

    private void ShakeCamera()
    {
        CameraShake cameraShake = FindObjectOfType<CameraShake>();
        StartCoroutine(cameraShake.Shake(0.25f, 1));
    }

    void ActivateDropper()
    {
        foreach (Dropper dropper in droppers)
        {
            dropper.enabled = true;
        }
    }

}