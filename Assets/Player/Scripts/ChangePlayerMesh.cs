using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerMesh : MonoBehaviour
{
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material fireMaterial;
    [SerializeField] Material iceMaterial;
    [SerializeField] Material electrikMaterial;
    [SerializeField] Material windMaterial;

    [SerializeField] float rotationSpeed = 5f;

    bool isCurrentlyRotating = false;
    public bool IsCurrentlyRotating { get { return isCurrentlyRotating; } }

    public void SetCurrentMaterial(int currentState, bool isEnabled)
    {
        Material currentMaterial = defaultMaterial;
        switch(currentState)
        {
            case 0:
                if(isEnabled) currentMaterial = fireMaterial;
                break;
            case 1:
                if(isEnabled) currentMaterial = iceMaterial;
                break;
            case 2:
                if(isEnabled) currentMaterial = electrikMaterial;
                break;
            case 3:
                if(isEnabled) currentMaterial = windMaterial;
                break;
        }
        GetComponent<MeshRenderer>().material = currentMaterial;
    }

    public IEnumerator RotatePlayerMesh(int value)
    {   
        isCurrentlyRotating = true;
        float rotationPercent = 0f;

        Quaternion currentLocation = transform.localRotation;
        Quaternion nextLocation = currentLocation;
        nextLocation.eulerAngles += new Vector3(0, value, 0);

        while(rotationPercent < 1)
        {
            rotationPercent += Time.deltaTime * rotationSpeed;
            transform.localRotation = Quaternion.Lerp(currentLocation, nextLocation, rotationPercent);
            yield return new WaitForEndOfFrame();
        }
        isCurrentlyRotating = false;
    }
}
