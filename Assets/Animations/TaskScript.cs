using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskScript : MonoBehaviour
{
    /**
     * Start timers and animations
     */
    public abstract void startTask();
    
    /**
     * Stop timers and animations
     */
    public abstract void stopTask();
    
    /**
     * returns true if task is done
     */
    public abstract bool progress();
}
