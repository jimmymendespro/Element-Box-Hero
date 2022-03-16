using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto3GifPlayer : MonoBehaviour
{
    [SerializeField] Image dashFramesHolder;
    [SerializeField] Sprite[] tutoDashFrames;
    [SerializeField] Image enemyKickFramesHolder;
    [SerializeField] Sprite[] tutoEnemyKickFrames;

    Tutorial tutorialUI;

    bool isDashTutoCycleComplete = true;
    bool isEnemyKickTutoCycleComplete = true;


    void Start()
    {
        tutorialUI = FindObjectOfType<Tutorial>();
    }

    
    void Update()
    {
        if(tutorialUI.IsDisplaying)
        {
            if(isDashTutoCycleComplete)
            {
                isDashTutoCycleComplete = false;
                StartCoroutine("DisplayTutoDash");
            }
            if(isEnemyKickTutoCycleComplete)
            {
                isEnemyKickTutoCycleComplete = false;
                StartCoroutine("DisplayTutoEnemyKick");
            }
        }
    }

    IEnumerator DisplayTutoDash()
    {
        for(int i = 0 ; i < tutoDashFrames.Length ; i++)
        {
            dashFramesHolder.sprite = tutoDashFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isDashTutoCycleComplete = true;
    }

    IEnumerator DisplayTutoEnemyKick()
    {
        for(int i = 0 ; i < tutoEnemyKickFrames.Length ; i++)
        {
            enemyKickFramesHolder.sprite = tutoEnemyKickFrames[i];
            yield return new WaitForSecondsRealtime(0.04f);
        }
        isEnemyKickTutoCycleComplete = true;
    }
}
