using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto1GifPlayer : MonoBehaviour
{
    [SerializeField] Image zsFramesHolder;
    [SerializeField] Sprite[] tutoZSFrames;
    [SerializeField] Image qdFramesHolder;
    [SerializeField] Sprite[] tutoQDFrames;
    [SerializeField] Image aeFramesHolder;
    [SerializeField] Sprite[] tutoAEFrames;
    Tutorial tutorialUI;

    bool isZSTutoCycleComplete = true;
    bool isQDTutoCycleComplete = true;
    bool isAETutoCycleComplete = true;

    void Start()
    {
        tutorialUI = FindObjectOfType<Tutorial>();
    }

    
    void Update()
    {
        if(tutorialUI.IsDisplaying)
        {
            if(isZSTutoCycleComplete)
            {
                isZSTutoCycleComplete = false;
                StartCoroutine("DisplayTutoZS");
            }
            if(isQDTutoCycleComplete)
            {
                isQDTutoCycleComplete = false;
                StartCoroutine("DisplayTutoQD");
            }
            if(isAETutoCycleComplete)
            {
                isAETutoCycleComplete = false;
                StartCoroutine("DisplayTutoAE");
            }
        }
    }

    IEnumerator DisplayTutoZS()
    {
        for(int i = 0 ; i < tutoZSFrames.Length ; i++)
        {
            zsFramesHolder.sprite = tutoZSFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isZSTutoCycleComplete = true;
    }

    IEnumerator DisplayTutoQD()
    {
        for(int i = 0 ; i < tutoQDFrames.Length ; i++)
        {
            qdFramesHolder.sprite = tutoQDFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isQDTutoCycleComplete = true;
    }

    IEnumerator DisplayTutoAE()
    {
        for(int i = 0 ; i < tutoAEFrames.Length ; i++)
        {
            aeFramesHolder.sprite = tutoAEFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isAETutoCycleComplete = true;
    }

}