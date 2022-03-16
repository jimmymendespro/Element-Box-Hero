using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto5GifPlayer : MonoBehaviour
{
    [SerializeField] Image iceModeFramesHolder;
    [SerializeField] Sprite[] tutoIceModeFrames;
    [SerializeField] Image iceDomeFramesHolder;
    [SerializeField] Sprite[] tutoIceDomeFrames;

    Tutorial tutorialUI;

    bool isIceModeTutoCycleComplete = true;
    bool isIceDomeTutoCycleComplete = true;


    void Start()
    {
        tutorialUI = FindObjectOfType<Tutorial>();
    }

    
    void Update()
    {
        if(tutorialUI.IsDisplaying)
        {
            if(isIceModeTutoCycleComplete)
            {
                isIceModeTutoCycleComplete = false;
                StartCoroutine("DisplayTutoIceMode");
            }
            if(isIceDomeTutoCycleComplete)
            {
                isIceDomeTutoCycleComplete = false;
                StartCoroutine("DisplayTutoIceDome");
            }
        }
    }

    IEnumerator DisplayTutoIceMode()
    {
        for(int i = 0 ; i < tutoIceModeFrames.Length ; i++)
        {
            iceModeFramesHolder.sprite = tutoIceModeFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isIceModeTutoCycleComplete = true;
    }

    IEnumerator DisplayTutoIceDome()
    {
        for(int i = 0 ; i < tutoIceDomeFrames.Length ; i++)
        {
            iceDomeFramesHolder.sprite = tutoIceDomeFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isIceDomeTutoCycleComplete = true;
    }

}