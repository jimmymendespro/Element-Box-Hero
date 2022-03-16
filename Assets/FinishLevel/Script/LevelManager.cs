using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject endLevelVFX;

    int currentSceneIndex;

    AudioSource audioSource;
    bool hasBeenReach = false;

    void Start() 
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(FindObjectOfType<PlayerUtilities>().IsPlayerTag(other.gameObject.tag))
        {
            if(!hasBeenReach)
            {
                hasBeenReach = true;
                audioSource.Play();
                GameObject vfx = Instantiate(endLevelVFX, transform.position, Quaternion.identity);
            }
            Invoke("LoadNextScene", 1.25f);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}