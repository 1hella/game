using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeineController : CharacterControllerScript
{
    public GameObject player;
    //public GameObject gameController;
    public float followDistance;
    //public Vector3 meetingPosition;
    private Animator animator;

    //private bool sitting = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SetAnimator(animator);
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    protected override void DefaultMovement()
    {
        float dist = Vector3.Distance(body.position, player.transform.position);
        if (dist > followDistance)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 keinePos = body.position;
            Vector2 distance = playerPos - keinePos;
            body.velocity = distance.normalized * speed;
            Vector2 movement = body.velocity;
            if (movement != Vector2.zero)
            {
                animator.SetFloat("XInput", movement.x);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            body.velocity = new Vector3(0, 0, 0);
            animator.SetBool("isWalking", false);
        }
    }

    /*
    public void WalkToEvent()
    {
        inEvent = true;
        float dist = Vector2.Distance(body.position, meetingPosition);
        //Debug.Log(dist);
        if (dist > 0.02)
        {
            body.velocity = ((Vector2)meetingPosition - body.position).normalized * speed;
        } else
        {
            body.velocity = Vector3.zero;
            inEvent = false;
            //gameController.GetComponent<GameController>().StartDialogue();
            eventController.DoneMovement();
        }
    }
    */
        
}
