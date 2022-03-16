using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    void Start() 
    {
        gameOverCanvas.enabled = false;
    }

    public void DisplayGameOver()
    {
        gameOverCanvas.enabled = true;
        FindObjectOfType<PlayerAction>().enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}