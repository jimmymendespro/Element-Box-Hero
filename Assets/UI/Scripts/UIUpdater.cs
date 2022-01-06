using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHealthUI;
    [SerializeField] TextMeshProUGUI firePowerUI;
    [SerializeField] TextMeshProUGUI icePowerUI;
    [SerializeField] TextMeshProUGUI electrikPowerUI;
    [SerializeField] TextMeshProUGUI windPowerUI;

    ChangePlayerState changePlayerState;
    
    void Start()
    {
        changePlayerState = FindObjectOfType<ChangePlayerState>();
        InitializeUI(100);
    }

    public void InitializeUI(int initialValue)
    {
        UpdatePlayerHealthUI(initialValue);
        UpdateFirePowerUI(changePlayerState.FireStateEnabled, initialValue);
        UpdateIcePowerUI(changePlayerState.IceStateEnabled, initialValue);
        UpdateElectrikPowerUI(changePlayerState.ElectrikStateEnabled, initialValue);
        UpdateWindPowerUI(changePlayerState.WindStateEnabled, initialValue);
    }

    public void UpdatePlayerHealthUI(int healthPoints)
    {
        playerHealthUI.text = $"Health : {healthPoints}";
    }

    public void UpdateFirePowerUI(bool isActive, int firePower)
    {
        if(isActive)
        {
            firePowerUI.text = $"Fire Power : {firePower}";
        }
        else
        {
            firePowerUI.enabled = false;
        }
    }

    public void UpdateIcePowerUI(bool isActive, int icePower)
    {
        if(isActive)
        {
            icePowerUI.text = $"Ice Power : {icePower}";
        }
        else
        {
            icePowerUI.enabled = false;
        }
    }

    public void UpdateElectrikPowerUI(bool isActive, int electrikPower)
    {
        if(isActive)
        {
            electrikPowerUI.text = $"Electrik Power : {electrikPower}";
        }
        else
        {
            electrikPowerUI.enabled = false;
        }
    }

    public void UpdateWindPowerUI(bool isActive, int windPower)
    {
        if(isActive)
        {
            windPowerUI.text = $"Wind Power : {windPower}";
        }
        else
        {
            windPowerUI.enabled = false;
        }
    }
}