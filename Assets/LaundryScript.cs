using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaundryScript : TaskScript
{
    // used for animating Keine
    public Animator animator;

    // used to count the number of animation events
    private int count = 0;
    // used to keep track of the number of events
    private const int MAX_COUNT = 3;
    // used to start spacebar listener
    private bool started = false;
    // used to prevent the next animation from starting
    private bool locked;
    // used to update user progress
    public Image progressBar;
    // used for the laundry task animations
    public UIController uiController;

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("space pressed");
                if (!locked && !IsFinished())
                {
                    count++;
                    if (count % 2 == 1)
                    {
                        animator.SetBool("ChopReset", false);
                        animator.SetBool("ChopStarted", true);
                        locked = true;
                    }
                    else
                    {
                        animator.SetBool("ChopReset", true);
                        animator.SetBool("ChopStarted", false);
                    }
                }
                else if (IsFinished())
                {
                    StopTask();
                } else
                {
                    locked = false;
                }
            }
        }
    }

    //called by anim event in the chop swing animation
    public void ChopDone()
    {
        progressBar.fillAmount = (float)(count + 1) / 2 / MAX_COUNT;
        locked = false;
        uiController.DoLaundryStep(count / 2 + 1);
    }

    public override void StartTask()
    {
        started = true;
        progressBar.fillAmount = 0;
        count = 0;
    }

    public override void StopTask()
    {
        progressBar.fillAmount = 100;
        animator.SetBool("ChopReset", true);
        animator.SetBool("ChopStarted", false);
        uiController.DoLaundryStep3();
    }

    public override bool IsFinished()
    {
        return count >= MAX_COUNT * 2;
    }

    public override bool Progress()
    {
        return false;
    }

    public override void ResetTask()
    {
        uiController.ResetLaundryRack();
    }
}
