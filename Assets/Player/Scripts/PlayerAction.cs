using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] float dashSpeed = 9f;
    [SerializeField] float dashLength = 9f;

    [Header("Fire state settings")]
    [SerializeField] ParticleSystem fireBall;
    [SerializeField] [Range(0,100)] int firePower = 100;
    [SerializeField] int fireBallCost = 20;

    [Header("Ice state settings")]
    [SerializeField] ParticleSystem iceDome;
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

    void Start() {
        uiUpdater = FindObjectOfType<UIUpdater>();
        changePlayerState = FindObjectOfType<ChangePlayerState>();
    }

    void Update()
    {
        Action();
        Dash();
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
                uiUpdater.UpdateFirePowerUI(true, firePower);
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
                uiUpdater.UpdateIcePowerUI(true, icePower);
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
                
                uiUpdater.UpdateElectrikPowerUI(true, electrikPower);
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

    void Dash()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            String input = "";
            if(Input.GetKey(KeyCode.Z))
            {
                input += "U";
            }
            if(Input.GetKey(KeyCode.S))
            {
                input += "D";
            }
            if(Input.GetKey(KeyCode.A))
            {
                input += "L";
            }
            if(Input.GetKey(KeyCode.E))
            {
                input += "R";
            }
            StartCoroutine("TranslatePlayerPosition", input);
        }
    }

    IEnumerator TranslatePlayerPosition(String inputPressed)
    {
        float movementPercentage = 0f;

        Vector3 playerRigPosition = transform.parent.transform.position;
        Vector3 destinationDirection = new Vector3(0, 0, 0);

        Debug.Log(inputPressed);

        for(int i = 0 ; i < inputPressed.Length ; i++)
        {
            switch(inputPressed[i])
            {
                case'U':
                    destinationDirection += transform.parent.transform.forward * dashLength;
                    break;
                case'D':
                    destinationDirection -= transform.parent.transform.forward * dashLength;
                    break;
                case'L':
                    destinationDirection -= transform.parent.transform.right * dashLength;
                    break;
                case'R':
                    destinationDirection += transform.parent.transform.right * dashLength;
                    break;
            }
        }

        Vector3 playerRigDestination = playerRigPosition + destinationDirection;

        while(movementPercentage < 1)
        {
            movementPercentage += Time.deltaTime * dashSpeed;
    
            transform.parent.transform.position = Vector3.Lerp(playerRigPosition, playerRigDestination, movementPercentage);
            yield return new WaitForEndOfFrame();
        }
    }

}