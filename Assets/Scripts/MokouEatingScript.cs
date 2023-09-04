using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MokouEatingScript : MonoBehaviour
{
    public bool isAlone = false;
    public Animator mokouAnimator;
    public EventController eventController;
    private bool calledFinish = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Initialize()
    {
        mokouAnimator.SetBool("laughing", false);
        calledFinish = false;
    }

    public void EndOfEating()
    {
        if (!isAlone)
        {
            mokouAnimator.SetBool("laughing", true);
        }
        else
        {
            //end the day
            if (!calledFinish)
            {
                calledFinish = true;
                eventController.FinishEating();
            }
            
            
        }
            
    }
    public void EndOfLaughing()
    {
        if (!calledFinish)
        {
            calledFinish = true;
            eventController.FinishEating();
        }
    }
}
