using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerState : MonoBehaviour
{
    [SerializeField] bool switchStateAllowed = true;
    [SerializeField] bool fireStateEnabled = false;
    [SerializeField] bool iceStateEnabled = false;
    [SerializeField] bool electrikStateEnabled = false;
    [SerializeField] bool windStateEnabled = false;

    int currentState = 0;
    int statesEnabled = 0;

    public int CurrentState { get { return currentState; } }
    public bool FireStateEnabled { get { return fireStateEnabled; } }
    public bool IceStateEnabled { get { return iceStateEnabled; } }
    public bool ElectrikStateEnabled { get { return electrikStateEnabled; } }
    public bool WindStateEnabled { get { return windStateEnabled; } }

    UIUpdater uIUpdater;

    void Start()
    {
        uIUpdater = FindObjectOfType<UIUpdater>();
        ChangeMaterial();
        StatesEnabledCalcul();
    }

    void Update()
    {
        ChangeState();
    }

    void StatesEnabledCalcul()
    {
        if(fireStateEnabled) statesEnabled++;
        if(iceStateEnabled) statesEnabled++;
        if(electrikStateEnabled) statesEnabled++;
        if(windStateEnabled) statesEnabled++;
    }

    void ChangeState()
    {
        if(switchStateAllowed)
        {
            if(!GetComponentInChildren<ChangePlayerMesh>().IsCurrentlyRotating)
            {
                if (Input.mouseScrollDelta.y < 0)
                {
                    StartCoroutine(GetComponentInChildren<ChangePlayerMesh>().RotatePlayerMesh(90));
                    IncrementingState();
                }
                else if (Input.mouseScrollDelta.y > 0)
                {
                    StartCoroutine(GetComponentInChildren<ChangePlayerMesh>().RotatePlayerMesh(-90));
                    DecrementingState();
                }
                if(Input.mouseScrollDelta.y != 0)
                {
                    ChangeMaterial();
                    GameObject.FindObjectOfType<UIUpdater>();
                    uIUpdater.UpdatePowers();
                } 
            }
        }
        
    }

    void IncrementingState()
    {
        currentState++;
        if(currentState > statesEnabled - 1) currentState = 0;
    }

    void DecrementingState()
    {
        currentState--;
        if(currentState < 0) currentState = statesEnabled - 1;
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
                else tag = "Player"; 
                break;
            case 1:
                if(iceStateEnabled)
                {
                    isEnabled = true;
                    tag = "PlayerIceMode";
                } 
                else tag = "Player"; 
                break;
            case 2:
                if(electrikStateEnabled)
                {
                    isEnabled = true;
                    tag = "PlayerElectrikMode";
                }
                else tag = "Player"; 
                break;
            case 3:
                if(windStateEnabled)
                {
                    isEnabled = true;
                    tag = "PlayerWindMode";
                }
                else tag = "Player"; 
                break;
        }
        GetComponentInChildren<ChangePlayerMesh>().SetCurrentMaterial(currentState, isEnabled);
    }

    public bool isFireStateSelected()
    {
        if(fireStateEnabled && currentState == 0) return true;
        else return false;
    }

    public bool isIceStateSelected()
    {
        if(iceStateEnabled && currentState == 1) return true;
        else return false;
    }

    public bool isElectrikStateSelected()
    {
        if(electrikStateEnabled && currentState == 2) return true;
        else return false;
    }

    public bool isWindStateSelected()
    {
        if(windStateEnabled && currentState == 3) return true;
        else return false;
    }

}