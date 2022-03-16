using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int currentSceneIndex;

    void Start() 
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void PlayAgainClicked()
    {
        ReloadLevel();
        FindObjectOfType<PlayerAction>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitClicked()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1;
    }
}
