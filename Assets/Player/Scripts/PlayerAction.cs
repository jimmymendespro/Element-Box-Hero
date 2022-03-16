using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAction : MonoBehaviour
{
    [Header("Fire state settings")]
    [SerializeField] ParticleSystem fireBall;
    [SerializeField] AudioClip fireBallSFX;
    [SerializeField] [Range(0,100)] int firePower = 100;
    [SerializeField] int fireBallCost = 20;

    [Header("Ice state settings")]
    [SerializeField] ParticleSystem iceDome;
    [SerializeField] AudioClip iceDomeSFX;
    [SerializeField] [Range(0,100)] int icePower = 100;
    [SerializeField] int iceDomeCost = 25;

    [Header("Elctrik state settings")]
    [SerializeField] ParticleSystem electrikShock;
    [SerializeField] [Range(0,100)] int electrikPower = 100;
    [SerializeField] [Range(0,0.1f)] float diminishingElectrikSpeed = 0.02f;

    [Header("Wind state settings")]
    [SerializeField] [Range(0,100)] int windPower = 100;
    
    public int FirePower { get { return firePower; } set { firePower = value; } }
    public int IcePower { get { return icePower; } set { icePower = value; } }
    public int ElectrikPower { get { return electrikPower; } set { electrikPower = value; } }
    public int WindPower { get { return windPower; } set { windPower = value; } }

    ChangePlayerState changePlayerState;
    UIUpdater uiUpdater;

    bool usingElectrikShock = false;
    public bool UsingElectrikShock { get { return usingElectrikShock; } }
    bool usingElectrikShockForValue = false;

    Vector3 dashDirection;

    AudioSource audioSource;

    void Start() {
        uiUpdater = FindObjectOfType<UIUpdater>();
        changePlayerState = FindObjectOfType<ChangePlayerState>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Action();
    }

    void Action()
    {
        switch(changePlayerState.CurrentState)
        {
        case 0:
            if(changePlayerState.FireStateEnabled) 
            {
                ActionFireState();
            }
            break;
        case 1:
            if(changePlayerState.IceStateEnabled) 
            {
                ActionIceState();
            }
            break;
        case 2:
            if(changePlayerState.ElectrikStateEnabled)
            {
                ActionElectrikState();
            }
            break;
        case 3:
            if(changePlayerState.WindStateEnabled)
            {
                ActionWindState();
            } 
            break;            
        }
    }

    void ActionFireState()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(firePower >= fireBallCost)
            {
                CastFireBall();
                audioSource.clip = fireBallSFX;
                audioSource.volume = 0.4f;
                audioSource.Play();
                firePower -= fireBallCost;
                uiUpdater.UpdateFirePowerUI(true, true, firePower);
            }
        }
    }

    void CastFireBall()
    {
        fireBall.Emit(1);
        fireBall.Play();
    }

    void ActionIceState()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(icePower >= iceDomeCost)
            {
                CastIceDome();
                audioSource.clip = iceDomeSFX;
                audioSource.volume = 0.4f;
                audioSource.Play();
                icePower -= iceDomeCost;
                uiUpdater.UpdateIcePowerUI(true, true, icePower);
            }
        }
    }

    void CastIceDome()
    {
        Instantiate(iceDome, transform.position, Quaternion.identity);
    }

    void ActionElectrikState()
    {
        if(Input.GetButton("Fire1"))
        {
            if(electrikPower > 0)
            {
                CastElectrikShock(true);
                usingElectrikShock = true;
                if(!usingElectrikShockForValue)
                {
                    StartCoroutine(DecreaseElectrikPower());
                    usingElectrikShockForValue = true;
                }
                
                uiUpdater.UpdateElectrikPowerUI(true, true, electrikPower);
            }
            else
            {
                CastElectrikShock(false);
                usingElectrikShock = false;
            }
        }
        else
        {
            CastElectrikShock(false);
            usingElectrikShock = false;
        }
    }

    IEnumerator DecreaseElectrikPower()
    {
        while(usingElectrikShock)
        {
            float decreasingSpeed = diminishingElectrikSpeed;
            electrikPower--;
            yield return new WaitForSeconds(diminishingElectrikSpeed);
        }
        usingElectrikShockForValue = false;
    }

    void CastElectrikShock(bool isActive)
    {
        var elctrikParticle = electrikShock.emission;
        elctrikParticle.enabled = isActive;
    }

    void ActionWindState()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Jump();
        }
    }

    void Jump()
    {
        
    }

}