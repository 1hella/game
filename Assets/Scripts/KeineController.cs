using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeineController : MonoBehaviour
{
    public GameObject player;
    public GameObject gameController;
    public Transform keineTransform;
    public float followDistance;
    public float speed;
    public Rigidbody2D body;
    public Vector3 offscreenPosition;
    public Vector3 meetingPosition;

    public bool canFollow = true;
    private bool sitting = false;
    private bool walkingToEvent = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (walkingToEvent)
        {
            WalkToEvent();
        }
        else
        {
            float dist = Vector3.Distance(keineTransform.position, player.transform.position);
            if (canFollow && !sitting && (dist > followDistance))
            {
                Vector2 playerPos = player.transform.position;
                Vector2 keinePos = keineTransform.position;
                body.velocity = (playerPos - keinePos).normalized * speed;
            }
            else
            {
                body.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    public void GoOffscreen()
    {
        body.position = offscreenPosition;
        canFollow = false;
    }

    public void WalkToEvent()
    {
        walkingToEvent = true;
        float dist = Vector2.Distance(body.position, meetingPosition);
        Debug.Log(dist);
        if (dist > 0.02)
        {
            body.velocity = ((Vector2)meetingPosition - body.position).normalized * speed;
        } else
        {
            body.velocity = Vector3.zero;
            walkingToEvent = false;
            gameController.GetComponent<GameController>().StartDialogue();
        }
    }
        
}
