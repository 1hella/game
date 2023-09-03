using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTestScript : TaskScript
{

    public Animator animator;
    private bool chopStarted = false;
    private bool chopDone = false;
    private int count = 0;
    private const int MAX_COUNT = 4 * 2 - 1; // todo: remove 2 when animation resets itself

    // Update is called once per frame
    void Update()
    {
    }

    //called by anim event in the chop swing animation
    public void ChopDone()
    {
        chopDone = true;
        chopStarted = false;
        stopTask();
    }

    public override void startTask()
    {
        // show and start the timers
    }

    public override void stopTask()
    {
        // stop the timers and hide them
    }

    public override bool progress()
    {
        // todo: reset the animation at the end of the frame.
        // for now, reset the animation on every second count
        if (!chopStarted && count < MAX_COUNT)
        {
            count++;
            if (count % 2 == 1)
            {
                animator.SetBool("ChopReset", false);
                animator.SetBool("ChopStarted", true);
                chopDone = false;
                chopStarted = true;
            } else
            {
                animator.SetBool("ChopReset", true);
                animator.SetBool("ChopStarted", false);
            }
        } else if (count >= MAX_COUNT)
        {
            return true;
        }
        return false;
    }
}
