using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto2GifPlayer : MonoBehaviour
{
    [SerializeField] Image tuto2FrameHolder;
    [SerializeField] Sprite[] tuto2Frames;

    Tutorial tutorialUI;

    bool isTuto2CycleComplete = true;

    void Start()
    {
        tutorialUI = GetComponent<Tutorial>();
    }

    
    void Update()
    {
        if(tutorialUI.IsDisplaying)
        {
            if(isTuto2CycleComplete)
            {
                isTuto2CycleComplete = false;
                StartCoroutine("DisplayTuto2");
            }
        }
    }

    IEnumerator DisplayTuto2()
    {
        for(int i = 0 ; i < tuto2Frames.Length ; i++)
        {
            tuto2FrameHolder.sprite = tuto2Frames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isTuto2CycleComplete = true;
    }

}