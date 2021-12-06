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
    
    void Start()
    {
        InitializeUI(100);
    }

    public void InitializeUI(int initialValue)
    {
        UpdatePlayerHealthUI(initialValue);
        UpdateFirePowerUI(initialValue);
        UpdateIcePowerUI(initialValue);
        UpdateElectrikPowerUI(initialValue);
        UpdateWindPowerUI(initialValue);
    }

    public void UpdatePlayerHealthUI(int healthPoints)
    {
        playerHealthUI.text = $"Health : {healthPoints}";
    }

    public void UpdateFirePowerUI(int firePower)
    {
        firePowerUI.text = $"Fire Power : {firePower}";
    }

    public void UpdateIcePowerUI(int icePower)
    {
        icePowerUI.text = $"Ice Power : {icePower}";
    }

    public void UpdateElectrikPowerUI(int electrikPower)
    {
        electrikPowerUI.text = $"Electrik Power : {electrikPower}";
    }

    public void UpdateWindPowerUI(int windPower)
    {
        windPowerUI.text = $"Wind Power : {windPower}";
    }
}