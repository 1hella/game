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
    public Vector3 lateMorningPosition;

    //private bool sitting = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SetAnimator(animator);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void DefaultMovement()
    {
        // ignore z values when calculating follow distance
        var bodyPosition = new Vector3(body.position.x, body.position.y, 0);
        var playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        
        float dist = Vector3.Distance(bodyPosition, playerPosition);
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

    public void FaceLeft()
    {
        animator.SetFloat("XInput", -1);
        animator.SetBool("isWalking", false);
    }

    public void GoToLateMorningPos()
    {
        canMoveFreely = false;
        body.position = lateMorningPosition;
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
