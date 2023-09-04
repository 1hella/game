using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControllerScript : MonoBehaviour
{

    public float speed;
    public Rigidbody2D body;
    public Vector3 initialPosition;
    public Vector3 foodPosition;
    public bool canMoveFreely = true; //should be false when game starts, it turns off for events and tasks
    protected bool inEvent = false;
    protected Vector2[] eventPositions;
    protected Vector2 currentEventPosition;
    private int currentPositionId = 0;
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
            //this character doesn't move for the event
            inEvent = false;
            eventController.DoneMovement();
            return;
        }
        eventPositions = newEventPositions;
        currentPositionId = 0;
        currentEventPosition = eventPositions[0];
        inEvent = true;
        canMoveFreely = false;
    }

    protected void EventMovement()
    {
        inEvent = true;
        float dist = Vector2.Distance(body.position, currentEventPosition);
        if (dist > 0.03)
        {
            body.velocity = (currentEventPosition - body.position).normalized * speed;
        }
        else
        {
            currentPositionId++;
            if (currentPositionId < eventPositions.Length)
            {
                currentEventPosition = eventPositions[currentPositionId];
            } else
            {
                body.velocity = Vector3.zero;
                inEvent = false;
                eventController.DoneMovement();
            }
            
        }
    }

    public void GoToMorningPos()
    {
        canMoveFreely = false;
        body.position = initialPosition;
        Debug.Log("position is now " + body.position);
    }

    public void GoToNightPos()
    {
        canMoveFreely = false;
        body.position = foodPosition;
    }
}
