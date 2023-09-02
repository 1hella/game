using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTestScript : MonoBehaviour
{

    //press 4 to play animation
    //once the anim is done, press 5 to go back to the start pose

    public Animator animator;
    private bool chopStarted = false;
    private bool chopDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && !chopStarted)
        {
            animator.SetBool("ChopReset", false);
            animator.SetBool("ChopStarted", true);
            chopDone = false;
            chopStarted = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && chopDone)
        {
            animator.SetBool("ChopStarted", false);
            animator.SetBool("ChopReset", true);
        }
    }

    //called by anim event in the chop swing animation
    public void ChopDone()
    {
        chopDone = true;
        chopStarted = false;
    }
}
