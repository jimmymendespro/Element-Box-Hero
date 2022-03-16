using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    [SerializeField] bool displayOnStart;

    bool isDisplaying = false;
    public bool IsDisplaying { get { return isDisplaying; } }

    bool hasBeenSeen = false;

    void Start() 
    {
        if(displayOnStart)
        {
            DisplayTutorial();
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(!displayOnStart && !hasBeenSeen)
        {
            DisplayTutorial();
        }
    }

    void DisplayTutorial()
    {
        Time.timeScale = 0;
        FindObjectOfType<PlayerAction>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isDisplaying = true;
        tutorial.SetActive(true);
    }

    public void TutorialSeen()
    {
        hasBeenSeen = true;
        Time.timeScale = 1;
        FindObjectOfType<PlayerAction>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isDisplaying = false;
        tutorial.SetActive(false);
    }

}