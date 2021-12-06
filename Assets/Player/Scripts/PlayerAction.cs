using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAction : MonoBehaviour
{
    [Header("Fire state settings")]
    [SerializeField] ParticleSystem fireBall;
    [SerializeField] [Range(0,100)] int firePower = 100;
    [SerializeField] int fireBallCost = 20;
    [SerializeField] [Range(0,5)]float fireReloadSpeed = 0.2f;

    [Header("Ice state settings")]
    [SerializeField] ParticleSystem iceDome;
    [SerializeField] [Range(0,100)] int icePower = 100;
    [SerializeField] int iceDomeCost = 25;
    [SerializeField] [Range(0,5)]float iceReloadSpeed = 0.2f;

    [Header("Elctrik state settings")]
    [SerializeField] ParticleSystem electrikShock;
    [SerializeField] [Range(0,100)] int electrikPower = 100;
    [SerializeField] [Range(0,0.1f)] float diminishingElectrikSpeed = 0.02f;
    [SerializeField] [Range(0,5)] float electrikReloadSpeed = 0.2f;

    [Header("Wind state settings")]
    [SerializeField] [Range(0,100)] int windPower = 100;

    ChangePlayerState changePlayerState;
    UIUpdater uiUpdater;

    bool reloadingFire = false;
    bool reloadingIce = false;
    bool reloadingElectrik = false;

    bool usingElectrikShock = false;
    bool usingElectrikShockForValue = false;

    void Start() {
        uiUpdater = FindObjectOfType<UIUpdater>();
        changePlayerState = FindObjectOfType<ChangePlayerState>();
    }

    void Update()
    {
        Action();
        Dash();
        ReloadElementPower();
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
                firePower -= fireBallCost;
                uiUpdater.UpdateFirePowerUI(firePower);
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
                icePower -= iceDomeCost;
                uiUpdater.UpdateIcePowerUI(icePower);
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
                
                uiUpdater.UpdateElectrikPowerUI(electrikPower);
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
        if(Input.GetButton("Fire1"))
        {
            Jump();
        }
    }

    void Jump()
    {
        
    }

    void Dash()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Click Droit");
        }
    }

    void ReloadElementPower()
    {
        if(firePower < 100)
        {
            if(!reloadingFire)
            {
                StartCoroutine(ReloadFirePower());
                reloadingFire = true;
            }
        }
        if(icePower < 100)
        {
            if(!reloadingIce)
            {
                StartCoroutine(ReloadIcePower());
                reloadingIce = true;
            }
        }
        if(electrikPower < 100)
        {
            if(!usingElectrikShock && !reloadingElectrik)
            {
                StartCoroutine(ReloadElectrikPower());
                reloadingElectrik = true;
            }
        }
        if(windPower < 100)
        {
            
        }
    }

    IEnumerator ReloadFirePower()
    {
        while(firePower != 100)
        {
            yield return new WaitForSeconds(fireReloadSpeed);
            firePower += 1;
            uiUpdater.UpdateFirePowerUI(firePower);
        }
        reloadingFire = false;
    }

    IEnumerator ReloadIcePower()
    {
        while(icePower != 100)
        {
            yield return new WaitForSeconds(iceReloadSpeed);
            icePower += 1;
            uiUpdater.UpdateIcePowerUI(icePower);
        }
        reloadingIce = false;
    }

    IEnumerator ReloadElectrikPower()
    {
        while(electrikPower != 100)
        {
            yield return new WaitForSeconds(electrikReloadSpeed);
            electrikPower += 1;
            uiUpdater.UpdateElectrikPowerUI(electrikPower);
        }
        reloadingElectrik = false;
    }

}