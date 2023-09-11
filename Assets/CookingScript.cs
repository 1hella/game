using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingScript : TaskScript
{
    // used for animating Keine
    public Animator characterAnimator;
    // used for the cauldren animations
    public Animator cauldrenAnimator;
    // used to update user progress
    public Image progressBar;
    // used for the cooking task animations
    public UIController uiController;
    

    // used to count the number of animation events
    private int count = 0;
    // used to keep track of the number of events
    private const int MAX_COUNT = 3 * 2;
    // used to start spacebar listener
    private bool started = false;
    // used to prevent the next animation from starting
    private bool locked;
    

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("space pressed");
                if (!locked && count < MAX_COUNT)
                {
                    count++;
                    if (count % 2 == 1)
                    {
                        progressBar.fillAmount = (float)count / MAX_COUNT;
                        characterAnimator.SetBool("ChopReset", false);
                        characterAnimator.SetBool("ChopStarted", true);
                        locked = true;
                    }
                    else
                    {
                        progressBar.fillAmount = (float)count / MAX_COUNT;
                        characterAnimator.SetBool("ChopReset", true);
                        characterAnimator.SetBool("ChopStarted", false);
                    }
                }
                else
                {
                    StopTask();
                }
            }
        }
    }

    //called by anim event in the chop swing animation
    public void ChopDone()
    {
        locked = false;
    }

    public override void StartTask()
    {
        started = true;
        progressBar.fillAmount = 0;
        count = 0;
        cauldrenAnimator.SetBool("isLit", true);
    }

    public override void StopTask()
    {
        progressBar.fillAmount = 100;
        characterAnimator.SetBool("ChopReset", true);
        characterAnimator.SetBool("ChopStarted", false);
        cauldrenAnimator.SetBool("isLit", false);
    }

    public override bool IsFinished()
    {
        return count >= MAX_COUNT;
    }

    public override bool Progress()
    {
        return false;
    }
}
