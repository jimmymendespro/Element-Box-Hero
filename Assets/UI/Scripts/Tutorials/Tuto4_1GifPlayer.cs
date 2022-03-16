using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto4_1GifPlayer : MonoBehaviour
{
    [SerializeField] Image fireBallFramesHolder;
    [SerializeField] Sprite[] tutoFireBallFrames;
    [SerializeField] Image ovenFramesHolder;
    [SerializeField] Sprite[] tutoOvenFrames;
    [SerializeField] Image activablePlatformFramesHolder;
    [SerializeField] Sprite[] tutoActivablePlatformFrames;
    Tutorial tutorialUI;

    bool isFireBallTutoCycleComplete = true;
    bool isOvenTutoCycleComplete = true;
    bool isActivablePlatformTutoCycleComplete = true;

    void Start()
    {
        tutorialUI = GetComponent<Tutorial>();
    }

    
    void Update()
    {
        if(tutorialUI.IsDisplaying)
        {
            if(isFireBallTutoCycleComplete)
            {
                isFireBallTutoCycleComplete = false;
                StartCoroutine("DisplayTutoFireBall");
            }
            if(isOvenTutoCycleComplete)
            {
                isOvenTutoCycleComplete = false;
                StartCoroutine("DisplayTutoOven");
            }
            if(isActivablePlatformTutoCycleComplete)
            {
                isActivablePlatformTutoCycleComplete = false;
                StartCoroutine("DisplayTutoActivablePlatform");
            }
        }
    }

    IEnumerator DisplayTutoFireBall()
    {
        for(int i = 0 ; i < tutoFireBallFrames.Length ; i++)
        {
            fireBallFramesHolder.sprite = tutoFireBallFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isFireBallTutoCycleComplete = true;
    }

    IEnumerator DisplayTutoOven()
    {
        for(int i = 0 ; i < tutoOvenFrames.Length ; i++)
        {
            ovenFramesHolder.sprite = tutoOvenFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isOvenTutoCycleComplete = true;
    }

    IEnumerator DisplayTutoActivablePlatform()
    {
        for(int i = 0 ; i < tutoActivablePlatformFrames.Length ; i++)
        {
            activablePlatformFramesHolder.sprite = tutoActivablePlatformFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isActivablePlatformTutoCycleComplete = true;
    }
}
