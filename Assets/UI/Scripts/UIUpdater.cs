using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [Header("Player Sprite UI Components")]
    [SerializeField] Sprite fireBoxTopLeft;
    [SerializeField] Sprite iceBoxTopLeft;
    [Header("Player Health UI Components")]
    [SerializeField] Button playerHealthBar;
    [SerializeField] TextMeshProUGUI playerHealthUI;
    [Header("Player Fire Power UI Components")]
    [SerializeField] TextMeshProUGUI firePowerUI;
    [SerializeField] Button firePowerBorder;
    [SerializeField] Image fireLogo;
    [SerializeField] Button fireBarBack;
    [SerializeField] Button fireBar;
    [Header("Player Ice Power UI Components")]
    [SerializeField] TextMeshProUGUI icePowerUI;
    [SerializeField] Button icePowerBorder;
    [SerializeField] Image iceLogo;
    [SerializeField] Button iceBarBack;
    [SerializeField] Button iceBar;
    [Header("Player Electrik Power UI Components")]
    [SerializeField] TextMeshProUGUI electrikPowerUI;
    [SerializeField] Button electrikPowerBorder;
    [SerializeField] Image electrikLogo;
    [SerializeField] Button electrikBarBack;
    [SerializeField] Button electrikBar;
    [Header("Player Wind Power UI Components")]
    [SerializeField] TextMeshProUGUI windPowerUI;
    [SerializeField] Button windPowerBorder;
    [SerializeField] Image windLogo;
    [SerializeField] Button windBarBack;
    [SerializeField] Button windBar;
    [Header("Dash UI Components")]
    [SerializeField] Image dashImage;
    [SerializeField] Sprite dashGreen;
    [SerializeField] Sprite dashRed;
    [SerializeField] Sprite dashBlack;
    [SerializeField] TextMeshProUGUI dashPowerUI;

    ChangePlayerState changePlayerState;
    PlayerAction playerAction;
    Dash dash;
    
    void Awake()
    {
        changePlayerState = FindObjectOfType<ChangePlayerState>();
        playerAction = FindObjectOfType<PlayerAction>();
        dash = FindObjectOfType<Dash>();
    }

    public void InitializeUI(int healthPoints, int powerPoints)
    {
        UpdatePlayerHealthUI(healthPoints);
        UpdateFirePowerUI(changePlayerState.FireStateEnabled, changePlayerState.isFireStateSelected(), powerPoints);
        UpdateIcePowerUI(changePlayerState.IceStateEnabled, changePlayerState.isIceStateSelected(),  powerPoints);
        UpdateElectrikPowerUI(changePlayerState.ElectrikStateEnabled, changePlayerState.isElectrikStateSelected(), powerPoints);
        UpdateWindPowerUI(changePlayerState.WindStateEnabled, changePlayerState.isWindStateSelected(), powerPoints);
        UpdateDashPowerUI(dash.DashUnlocked, powerPoints);
    }

    public void UpdateTopLeftSprite(int currentState, bool stateEnabled)
    {
        GameObject.FindGameObjectWithTag("TopBox");
    }

    public void UpdatePlayerHealthUI(int healthPoints)
    {
        playerHealthUI.text = $"Health : {healthPoints}";
        UpdateHealthBarColor(healthPoints);
        UpdateHealthBarSizeAndPosition(healthPoints);
    }

    void UpdateHealthBarColor(int healthPoints)
    {
        ColorBlock test = playerHealthBar.colors;
        if (healthPoints > 50)
        {
            test.disabledColor = Color.green;
        }
        else if (healthPoints > 25)
        {
            test.disabledColor = new Color(1, 0.909f, 0, 1); // Jaune
        }
        else
        {
            test.disabledColor = Color.red;
        }
        playerHealthBar.colors = test;
    }

    void UpdateHealthBarSizeAndPosition(int healthPoints)
    {
        RectTransform healthBarRectTransform = playerHealthBar.GetComponent<RectTransform>();
        healthBarRectTransform.sizeDelta = new Vector2(healthPoints * 5, 50);
        // Permet d'avoir la barre de vie qui reste dans son décors
        healthBarRectTransform.anchoredPosition = new Vector2(500 - (500 - healthPoints * 5)/2 , -35); // 500 est la position en x de la barre de vie.
    }

    public void UpdatePowers()
    {
        UpdateFirePowerUI(changePlayerState.FireStateEnabled, changePlayerState.isFireStateSelected(), playerAction.FirePower);
        UpdateIcePowerUI(changePlayerState.IceStateEnabled, changePlayerState.isIceStateSelected(), playerAction.IcePower);
        UpdateElectrikPowerUI(changePlayerState.ElectrikStateEnabled, changePlayerState.isElectrikStateSelected(),  0);
        UpdateWindPowerUI(changePlayerState.WindStateEnabled, changePlayerState.isWindStateSelected(), 0);
    }

    public void UpdateFirePowerUI(bool isActive, bool isSelected, int firePower)
    {
        if(isSelected)
        {
            firePowerBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(402, 150);
            firePowerBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(199, -275);
            fireLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(99.4f, 105);
            fireLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 25);
            fireBarBack.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 30);
            fireBar.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 30);
            firePowerUI.text = $"Fire Power : {firePower}";
        }
        else if(isActive)
        {
            firePowerBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(202, 150);
            firePowerBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(99, -275);
            fireLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(99.4f, 105);
            fireLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 25);
            fireBarBack.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            fireBar.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            firePowerUI.text = $"Fire Power : {firePower}";
        }
        else
        {
            fireLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(142, 150);
            fireLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            fireBarBack.gameObject.SetActive(false);
            fireBar.gameObject.SetActive(false);
            firePowerUI.enabled = false;
        }
        UpdateFireBarSizeAndPosition(isActive, isSelected, firePower);
    }

    void UpdateFireBarSizeAndPosition(bool isActive, bool isSelected, int firePoints)
    {
        RectTransform fireBarRectTransform = fireBar.GetComponent<RectTransform>();
        if(isSelected)
        {
            fireBarRectTransform.sizeDelta = new Vector2(firePoints * 3.6f, 30);
            fireBarRectTransform.anchoredPosition = new Vector2(-(360 - firePoints * 3.6f)/2 , -40);
        }
        else if(isActive)
        {
            fireBarRectTransform.sizeDelta = new Vector2(firePoints * 1.6f, 30);
            fireBarRectTransform.anchoredPosition = new Vector2(-(160 - firePoints * 1.6f)/2 , -40);
        }
    }

    public void UpdateIcePowerUI(bool isActive, bool isSelected, int icePower)
    {
        if(isSelected)
        {
            icePowerBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(402, 150);
            icePowerBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(199, -440);
            iceLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(99.4f, 105);
            iceLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 25);
            iceBarBack.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 30);
            iceBar.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 30);
            icePowerUI.text = $"Ice Power : {icePower}";
        }
        else if(isActive)
        {
            icePowerBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(202, 150);
            icePowerBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(99, -440);
            iceLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(99.4f, 105);
            iceLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 25);
            iceBarBack.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            iceBar.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            icePowerUI.text = $"Ice Power : {icePower}";
        }
        else
        {
            iceLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(142, 150);
            iceLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            iceBarBack.gameObject.SetActive(false);
            iceBar.gameObject.SetActive(false);
            icePowerUI.enabled = false;
        }
        UpdateIceBarSizeAndPosition(isActive, isSelected, icePower);
    }

    void UpdateIceBarSizeAndPosition(bool isActive, bool isSelected, int icePoints)
    {
        RectTransform iceBarRectTransform = iceBar.GetComponent<RectTransform>();
        if(isSelected)
        {
            iceBarRectTransform.sizeDelta = new Vector2(icePoints * 3.6f, 30);
            iceBarRectTransform.anchoredPosition = new Vector2(-(360 - icePoints * 3.6f)/2 , -40);
        }
        else if(isActive)
        {
            iceBarRectTransform.sizeDelta = new Vector2(icePoints * 1.6f, 30);
            iceBarRectTransform.anchoredPosition = new Vector2(-(160 - icePoints * 1.6f)/2 , -40);
        }
    }

    public void UpdateElectrikPowerUI(bool isActive, bool isSelected, int electrikPower)
    {
        if(isSelected)
        {
            electrikPowerBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(402, 150);
            electrikPowerBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(199, -605);
            electrikLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(99.4f, 105);
            electrikLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 25);
            electrikBarBack.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 30);
            electrikBar.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 30);
            electrikPowerUI.text = $"Electrik Power : {electrikPower}";
        }
        else if(isActive)
        {
            electrikPowerBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(202, 150);
            electrikPowerBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(99, -605);
            electrikLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(99.4f, 105);
            electrikLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 25);
            electrikBarBack.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            electrikBar.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            electrikPowerUI.text = $"Electrik Power : {electrikPower}";
        }
        else
        {
            electrikLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(142, 150);
            electrikLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            electrikBarBack.gameObject.SetActive(false);
            electrikBar.gameObject.SetActive(false);
            electrikPowerUI.enabled = false;
        }
    }

    public void UpdateWindPowerUI(bool isActive, bool isSelected, int windPower)
    {
        if(isSelected)
        {
            windPowerBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(402, 150);
            windPowerBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(199, -770);
            windLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(99.4f, 105);
            windLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 25);
            windBarBack.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 30);
            windBar.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 30);
            windPowerUI.text = $"Wind Power : {windPower}";
        }
        else if(isActive)
        {
            windPowerBorder.GetComponent<RectTransform>().sizeDelta = new Vector2(202, 150);
            windPowerBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(99, -770);
            windLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(99.4f, 105);
            windLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 25);
            windBarBack.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            windBar.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            windPowerUI.text = $"Wind Power : {windPower}";
        }
        else
        {
            windLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(142, 150);
            windLogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            windBarBack.gameObject.SetActive(false);
            windBar.gameObject.SetActive(false);
            windPowerUI.enabled = false;
        }
    }

    public void UpdateDashPowerUI(bool isActive, int dashPower)
    {
        if(isActive)
        {
            dashPowerUI.text = $"Dash : {dashPower}";
            if(dashPower > 50) 
            {
                dashImage.sprite = dashGreen;
                dashPowerUI.color = Color.green;
            }
            else 
            {
                dashImage.sprite = dashRed;
                dashPowerUI.color = Color.red;
            }
        }
        else
        {
            dashPowerUI.enabled = false;
            dashImage.enabled = false;
        }
    }

    public void SetDashToBlack()
    {
        dashImage.sprite = dashBlack;
    }
}