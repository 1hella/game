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
    private const int MAX_COUNT = 3 * 2;
    // used to start spacebar listener
    private bool started = false;
    // used to prevent the next animation from starting
    private bool locked;
    // used to update user progress
    public Image progressBar;

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!locked && count < MAX_COUNT)
                {
                    count++;
                    if (count % 2 == 1)
                    {
                        animator.SetBool("ChopReset", false);
                        animator.SetBool("ChopStarted", true);
                        progressBar.fillAmount = (float)count / MAX_COUNT;
                        locked = true;
                    }
                    else
                    {
                        animator.SetBool("ChopReset", true);
                        animator.SetBool("ChopStarted", false);
                        progressBar.fillAmount = (float)count / MAX_COUNT;
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
    }

    public override void StopTask()
    {
        progressBar.fillAmount = 100;
        animator.SetBool("ChopReset", true);
        animator.SetBool("ChopStarted", false);
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
