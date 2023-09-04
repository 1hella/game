using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskScript : MonoBehaviour
{
    /**
     * Start timers and animations
     */
    public abstract void StartTask();
    
    /**
     * Stop timers and animations
     */
    public abstract void StopTask();
    
    /**
     * returns true if task is done
     */
    public abstract bool Progress();

    public abstract bool IsFinished();
}
