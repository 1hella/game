using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControllerScript : MonoBehaviour
{

    public float speed;
    public Rigidbody2D body;
    public Vector3 initialPosition;
    public bool canMoveFreely = true; //should be false when game starts, it turns off for events and tasks
    protected bool inEvent = false;
    protected Vector2[] eventPositions;
    protected Vector2 nextEventPosition;
    public EventController eventController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if (inEvent)
        {
            EventMovement();
        }
        else
        {
            if (canMoveFreely)
            {
                DefaultMovement();
            }
        }
    }

    protected abstract void DefaultMovement();

    public void BeginEvent(Vector2[] newEventPositions)
    {
        if (newEventPositions == null)
        {
            inEvent = false;
            eventController.DoneMovement();
        }
        eventPositions = newEventPositions;
        inEvent = true;
        canMoveFreely = false;
    }

    protected void EventMovement()
    {
        inEvent = true;
        float dist = Vector2.Distance(body.position, nextEventPosition);
        //Debug.Log(dist);
        if (dist > 0.02)
        {
            body.velocity = (nextEventPosition - body.position).normalized * speed;
        }
        else
        {
            body.velocity = Vector3.zero;
            inEvent = false;
            eventController.DoneMovement();
        }
    }

    public void GoToMorningPos()
    {
        canMoveFreely = false;
        body.position = initialPosition;
    }
}
