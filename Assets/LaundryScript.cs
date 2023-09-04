using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaundryScript : TaskScript
{

    public Animator animator;

    private int count = 0;
    private const int MAX_COUNT = 3 * 2; // todo: remove 2 when animation resets itself
    private bool started = false;
    private bool locked;

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
                        locked = true;
                    }
                    else
                    {
                        animator.SetBool("ChopReset", true);
                        animator.SetBool("ChopStarted", false);
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
    }

    public override void StopTask()
    {
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
