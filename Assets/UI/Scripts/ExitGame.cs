using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    [SerializeField] Canvas exitGame;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            FindObjectOfType<PlayerAction>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            exitGame.gameObject.SetActive(true);
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameReturn()
    {
        Time.timeScale = 1;
        exitGame.gameObject.SetActive(false);
        if(!TutorialIsDisplaying())
        {
            FindObjectOfType<PlayerAction>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    bool TutorialIsDisplaying()
    {
        Tutorial[] levelTutorials = FindObjectsOfType<Tutorial>();
        foreach(Tutorial tutorial in levelTutorials)
        {
            if(tutorial.IsDisplaying)
            {
                return true;
            }
        }
        return false;
    }

}