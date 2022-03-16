using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    [SerializeField] [Range(0,5)]float dashReloadSpeed = 0.05f;
    [SerializeField] [Range(0,5)]float fireReloadSpeed = 0.2f;
    [SerializeField] [Range(0,5)]float iceReloadSpeed = 0.2f;
    [SerializeField] [Range(0,5)]float electrikReloadSpeed = 0.2f;

    PlayerAction playerAction;
    Dash dash;
    ChangePlayerState changePlayerState;
    UIUpdater uiUpdater;

    bool reloadingDash = false;
    bool reloadingFire = false;
    bool reloadingIce = false;
    bool reloadingElectrik = false;

    void Start() 
    {
        playerAction = FindObjectOfType<PlayerAction>();
        dash = GetComponent<Dash>();
        changePlayerState = FindObjectOfType<ChangePlayerState>();
        uiUpdater = FindObjectOfType<UIUpdater>();
    }

    void Update()
    {
        ReloadDash();
        ReloadElementPower();
    }

    void ReloadDash()
    {
        if(dash.DashPower < 100)
        {
            if(!reloadingDash)
            {
                StartCoroutine(ReloadDashPower());
                reloadingDash = true;
            }
        }
    }

    IEnumerator ReloadDashPower()
    {
        while(dash.DashPower != 100)
        {
            if(dash.Fire2Clicked)
            {
                dash.Fire2Clicked = false;
                yield return new WaitForSeconds(1);
            }
            dash.DashPower += 1;
            if(!dash.DashFullyUsed)
            {
                uiUpdater.UpdateDashPowerUI(dash.DashUnlocked, dash.DashPower);
            }
            else
            {
                uiUpdater.SetDashToBlack();
            }
            yield return new WaitForSeconds(dashReloadSpeed);
        }
        reloadingDash = false;
        dash.DashFullyUsed = false;
        uiUpdater.UpdateDashPowerUI(dash.DashUnlocked, dash.DashPower);
    }

    void ReloadElementPower()
    {
        if(playerAction.FirePower < 100)
        {
            if(!reloadingFire)
            {
                StartCoroutine(ReloadFirePower());
                reloadingFire = true;
            }
        }
        if(playerAction.IcePower < 100)
        {
            if(!reloadingIce)
            {
                StartCoroutine(ReloadIcePower());
                reloadingIce = true;
            }
        }
        if(playerAction.ElectrikPower < 100)
        {
            if(!playerAction.UsingElectrikShock && !reloadingElectrik)
            {
                StartCoroutine(ReloadElectrikPower());
                reloadingElectrik = true;
            }
        }
        if(playerAction.WindPower < 100)
        {
            
        }
    }

    IEnumerator ReloadFirePower()
    {
        while(playerAction.FirePower < 100)
        {
            yield return new WaitForSeconds(fireReloadSpeed);
            playerAction.FirePower += 1;
            uiUpdater.UpdateFirePowerUI(true, changePlayerState.isFireStateSelected(), playerAction.FirePower);
        }
        reloadingFire = false;
    }

    IEnumerator ReloadIcePower()
    {
        while(playerAction.IcePower != 100)
        {
            yield return new WaitForSeconds(iceReloadSpeed);
            playerAction.IcePower += 1;
            uiUpdater.UpdateIcePowerUI(true, changePlayerState.isIceStateSelected(), playerAction.IcePower);
        }
        reloadingIce = false;
    }

    IEnumerator ReloadElectrikPower()
    {
        while(playerAction.ElectrikPower != 100)
        {
            yield return new WaitForSeconds(electrikReloadSpeed);
            playerAction.ElectrikPower += 1;
            uiUpdater.UpdateElectrikPowerUI(true, changePlayerState.isElectrikStateSelected(), playerAction.ElectrikPower);
        }
        reloadingElectrik = false;
    }

    public void FireRegeneration(int firePoint = 1)
    {
        if(playerAction.FirePower + firePoint > 100)
        {
            playerAction.FirePower = 100;
        }
        else {
            playerAction.FirePower += firePoint;
        }

        uiUpdater.UpdateFirePowerUI(true, true, playerAction.FirePower);
    }
}
