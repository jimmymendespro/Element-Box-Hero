using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    [SerializeField] [Range(0,5)]float fireReloadSpeed = 0.2f;
    [SerializeField] [Range(0,5)]float iceReloadSpeed = 0.2f;
    [SerializeField] [Range(0,5)] float electrikReloadSpeed = 0.2f;

    PlayerAction playerAction;
    UIUpdater uiUpdater;

    bool reloadingFire = false;
    bool reloadingIce = false;
    bool reloadingElectrik = false;

    void Start() 
    {
        playerAction = FindObjectOfType<PlayerAction>();
        uiUpdater = FindObjectOfType<UIUpdater>();
    }

    void Update() 
    {
        ReloadElementPower();
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
        while(playerAction.FirePower != 100)
        {
            yield return new WaitForSeconds(fireReloadSpeed);
            playerAction.FirePower += 1;
            uiUpdater.UpdateFirePowerUI(true, playerAction.FirePower);
        }
        reloadingFire = false;
    }

    IEnumerator ReloadIcePower()
    {
        while(playerAction.IcePower != 100)
        {
            yield return new WaitForSeconds(iceReloadSpeed);
            playerAction.IcePower += 1;
            uiUpdater.UpdateIcePowerUI(true, playerAction.IcePower);
        }
        reloadingIce = false;
    }

    IEnumerator ReloadElectrikPower()
    {
        while(playerAction.ElectrikPower != 100)
        {
            yield return new WaitForSeconds(electrikReloadSpeed);
            playerAction.ElectrikPower += 1;
            uiUpdater.UpdateElectrikPowerUI(true, playerAction.ElectrikPower);
        }
        reloadingElectrik = false;
    }
}
