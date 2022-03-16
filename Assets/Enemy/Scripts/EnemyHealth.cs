using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] Button ennemyHealthBar;

    int initialHealth;

    RectTransform healthBarRectTransform;

    private void Start() 
    {
        initialHealth = health;
        healthBarRectTransform = ennemyHealthBar.GetComponent<RectTransform>();
    }

    public void DamageTaken(int damage)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void RestaureHealth(int restaureAmount)
    {
        if(health + restaureAmount <= initialHealth)
        {
            health += restaureAmount;
        }
        else
        {
            health = initialHealth;
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        UpdateHealthBarSizeAndPosition();
        UpdateHealthBarColor();
    }

    // Ici, ennemyHealthBar est de type Button, width = 160, height = 30
    private void UpdateHealthBarSizeAndPosition()
    {
        ennemyHealthBar.transform.parent.gameObject.SetActive(true);
        healthBarRectTransform.sizeDelta = new Vector2(health * (float)160/initialHealth, 30); // 160/initialHealth correspond à la taille de 1HP 
        healthBarRectTransform.anchoredPosition = new Vector2(-(160 * 0.025f - health * (float)160/initialHealth * 0.025f)/2, 0); // 0.025 est le scaling en x de healthBar
    }

    void UpdateHealthBarColor()
    {
        ColorBlock enemyHealthColorBlock = ennemyHealthBar.colors;
        if (health > initialHealth/2)
        {
            enemyHealthColorBlock.disabledColor = Color.green;
        }
        else if (health > initialHealth/4)
        {
            enemyHealthColorBlock.disabledColor = new Color(1, 0.909f, 0, 1); // Jaune
        }
        else
        {
            enemyHealthColorBlock.disabledColor = Color.red;
        }
        ennemyHealthBar.colors = enemyHealthColorBlock;
    }
}
