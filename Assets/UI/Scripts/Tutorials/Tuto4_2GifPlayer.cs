using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto4_2GifPlayer : MonoBehaviour
{
    [SerializeField] Image regenerationOvenFramesHolder;
    [SerializeField] Sprite[] tutoRegenerationOvenFrames;
    Tutorial tutorialUI;

    bool isRegenerationOvenTutoCycleComplete = true;

    void Start()
    {
        tutorialUI = GetComponent<Tutorial>();
    }

    
    void Update()
    {
        if(tutorialUI.IsDisplaying)
        {
            if(isRegenerationOvenTutoCycleComplete)
            {
                isRegenerationOvenTutoCycleComplete = false;
                StartCoroutine("DisplayTutoRegenerationOven");
            }
        }
    }

    IEnumerator DisplayTutoRegenerationOven()
    {
        for(int i = 0 ; i < tutoRegenerationOvenFrames.Length ; i++)
        {
            regenerationOvenFramesHolder.sprite = tutoRegenerationOvenFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isRegenerationOvenTutoCycleComplete = true;
    }

}