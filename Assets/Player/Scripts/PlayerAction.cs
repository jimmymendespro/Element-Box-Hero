using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] bool fireModeEnabled = false;
    [SerializeField] bool iceModeEnabled = false;
    [SerializeField] bool electrikModeEnabled = false;
    [SerializeField] bool windModeEnabled = false;

    [SerializeField] ParticleSystem fireBall;
    [SerializeField] ParticleSystem iceDome;
    [SerializeField] ParticleSystem electrikShock;

    /*[SerializeField] [Range(0,100)] int firePower = 100;
    [SerializeField] [Range(0,100)] int icePower = 100;
    [SerializeField] [Range(0,100)] int electrikPower = 100;
    [SerializeField] [Range(0,100)] int windPower = 100;*/

    int currentState = 0;

    void Start() {
        ChangeMaterial();
    }

    void Update()
    {
        Action();
        ChangeState();
        Dash();
    }

    private void Action()
    {
        if (Input.GetButton("Fire1"))
        {
            switch(currentState)
            {
            case 0:
                if(fireModeEnabled) 
                {
                    CastFireBall(true);
                }
                break;
            case 1:
                if(iceModeEnabled) 
                {
                    CastIceDome();
                }
                break;
            case 2:
                if(electrikModeEnabled)
                {
                    CastElectrikShock(true);
                }
                break;
            case 3:
                if(windModeEnabled) Jump();
                break;
            }
        }
        else
        {
            CastFireBall(false);
            CastElectrikShock(false);
        }
    }

    void ChangeState()
    {
        if(!GetComponentInChildren<TransformPlayerMesh>().IsCurrentlyRotating)
        {
            if (Input.mouseScrollDelta.y > 0)
        {
            StartCoroutine(GetComponentInChildren<TransformPlayerMesh>().ChangingStateRotation(90));
            IncrementingState();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            StartCoroutine(GetComponentInChildren<TransformPlayerMesh>().ChangingStateRotation(-90));
            DecrementingState();
        }
        if(Input.mouseScrollDelta.y != 0) ChangeMaterial();
        }
    }

    void Dash()
    {
        
    }

    void CastFireBall(bool isActive)
    {
        var fireParticle = fireBall.emission;
        fireParticle.enabled = isActive;
    }

    void CastIceDome()
    {
        Instantiate(iceDome, transform.position, Quaternion.identity);
    }

    void CastElectrikShock(bool isActive)
    {
        var elctrikParticle = electrikShock.emission;
        elctrikParticle.enabled = isActive;
    }

    void Jump()
    {
        
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
                if(fireModeEnabled) 
                {
                    isEnabled = true;
                    tag = "PlayerFireMode"; 
                }
                break;
            case 1:
                if(iceModeEnabled)
                {
                    isEnabled = true;
                    tag = "PlayerIceMode";
                } 
                break;
            case 2:
                if(electrikModeEnabled)
                {
                    isEnabled = true;
                    tag = "PlayerElectrikMode";
                } 
                break;
            case 3:
                if(windModeEnabled)
                {
                    isEnabled = true;
                    tag = "PlayerWindMode";
                }
                break;
        }
        GetComponentInChildren<TransformPlayerMesh>().SetCurrentMaterial(currentState, isEnabled);
    }

}
