using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerState : MonoBehaviour
{
    [SerializeField] bool fireStateEnabled = false;
    [SerializeField] bool iceStateEnabled = false;
    [SerializeField] bool electrikStateEnabled = false;
    [SerializeField] bool windStateEnabled = false;

    int currentState = 0;

    public int CurrentState { get { return currentState; } }
    public bool FireStateEnabled { get { return fireStateEnabled; } }
    public bool IceStateEnabled { get { return iceStateEnabled; } }
    public bool ElectrikStateEnabled { get { return electrikStateEnabled; } }
    public bool WindStateEnabled { get { return windStateEnabled; } }

    void Start()
    {
        ChangeMaterial();
    }

    void Update()
    {
        ChangeState();
    }

    void ChangeState()
    {
        if(!GetComponentInChildren<ChangePlayerMesh>().IsCurrentlyRotating)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                StartCoroutine(GetComponentInChildren<ChangePlayerMesh>().RotatePlayerMesh(90));
                IncrementingState();
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                StartCoroutine(GetComponentInChildren<ChangePlayerMesh>().RotatePlayerMesh(-90));
                DecrementingState();
            }
            if(Input.mouseScrollDelta.y != 0) ChangeMaterial();
        }
    }

    void IncrementingState()
    {
        currentState++;
        if(currentState > 3) currentState = 0;
    }

    void DecrementingState()
    {
        currentState--;
        if(currentState < 0) currentState = 3;
    }

    void ChangeMaterial()
    {
        bool isEnabled = false;
        switch(currentState)
        {
            case 0:
                if(fireStateEnabled) 
                {
                    isEnabled = true;
                    tag = "PlayerFireMode"; 
                }
                break;
            case 1:
                if(iceStateEnabled)
                {
                    isEnabled = true;
                    tag = "PlayerIceMode";
                } 
                break;
            case 2:
                if(electrikStateEnabled)
                {
                    isEnabled = true;
                    tag = "PlayerElectrikMode";
                } 
                break;
            case 3:
                if(windStateEnabled)
                {
                    isEnabled = true;
                    tag = "PlayerWindMode";
                }
                break;
        }
        GetComponentInChildren<ChangePlayerMesh>().SetCurrentMaterial(currentState, isEnabled);
    }
}
