using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BambooScript : TaskScript
{

    public Animator characterAnimator;
    public Animator bambooAnimator;
    public UIController uiController;
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
                    characterAnimator.SetBool("ChopReset", false);
                    characterAnimator.SetBool("ChopStarted", true);
                    bambooAnimator.SetBool("isCut", true);
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
        characterAnimator.SetBool("ChopReset", true);
        characterAnimator.SetBool("ChopStarted", false);
        uiController.ShowBambooPile();
    }

    public override bool IsFinished()
    {
        Debug.Log(input);
        return input == 100;
    }

    public override bool Progress()
    {
        return false;
    }

    public override void ResetTask()
    {
        uiController.ResetBambooPile();
        bambooAnimator.SetBool("isCut", false);
    }
}
