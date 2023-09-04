using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ChopTestScript : TaskScript
{

    public Animator animator;
    private bool chopStarted = false;
    private bool chopDone = false;
    private int count = 0;
    private const int MAX_COUNT = 3 * 2 - 1; // todo: remove 2 when animation resets itself
    private bool started = false;
    private float input;
    public Image progressBar;
    private bool keyDown;

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                keyDown = false;
                Debug.Log(progressBar.fillAmount);
                if (progressBar.fillAmount < 1)
                {
                    input = 0;
                }
                else
                {
                    animator.SetBool("ChopReset", false);
                    animator.SetBool("ChopStarted", true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                keyDown = true;
            }
            if (keyDown)
            {
                input += .75f * Time.deltaTime;
                if (input > 100)
                {
                    input = 100;
                }
                
            }
            progressBar.fillAmount = input;
        }
    }

    //called by anim event in the chop swing animation
    public void ChopDone()
    {
        chopDone = true;
        chopStarted = false;
        StopTask();
    }

    public override void StartTask()
    {
        started = true;
        progressBar.fillAmount = 0;
        input = 0;
    }

    public override void StopTask()
    {
        started = false;
        progressBar.fillAmount = 100;
        input = 100;
        animator.SetBool("ChopReset", true);
        animator.SetBool("ChopStarted", false);
    }

    public override bool IsFinished()
    {
        Debug.Log(input);
        return input == 100;
    }

    public override bool Progress()
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
