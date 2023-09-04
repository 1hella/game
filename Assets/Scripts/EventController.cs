using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{

    public PlayerController playerController;
    public KeineController keineController;
    public GameController gameController;
    public GameObject player;
    public GameObject keine;
    public GameObject mokouEating;
    public bool keineAlive = true;

    public DialogueManager dialogController;
    private int nextDiagId = 0;
    private bool eventHasDiag = false;
    private int doneCounter = 0;
    private int morningTestCounter = 0; //ONLY FOR DEBUG, REMOVE LATER

    enum EventType
    {
        Morning,
        FoodTime,
        PostFood,
        Exit
    }
    private EventType currentEvent;

    private Vector2[] keineMorningTarget = {
        new Vector2(0, -1.9f)
    };

    private Vector2[] mokouFoodTimeTargets = {
        new Vector2(-3.8f, -0.6f),
        new Vector2(-3.4f, 3.4f),
        //new Vector2(-2.6f, 2.1f)
    };

    private Vector2[] keineFoodTimeTargets = {
        new Vector2(-3.8f, -0.6f),
        new Vector2(-3.4f, 2.1f),
        new Vector2(-1.1f, 2.1f)
    };

    /*
    private Vector2[] mokouAfterFoodTargets = {
        new Vector2(-3.4f, 2.1f),
        new Vector2(-3.8f, -0.6f),
        new Vector2(-2.1f, -1.9f)
    };

    private Vector2[] keineAfterFoodTargets = {
        new Vector2(-3.4f, 2.1f),
        new Vector2(-3.8f, -0.6f),
        new Vector2(0, -1.9f)
    };

    private Vector2[] keineExitTarget = {
        new Vector2(3.4f, -6)
    };
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DoMorningEvent()
    {
        currentEvent = EventType.Morning;
        playerController.GoToMorningPos();
        keineController.GoToMorningPos();
        doneCounter = 0;
        keineController.BeginEvent(keineMorningTarget);
        playerController.BeginEvent(null);
        eventHasDiag = true;
    }

    public void DoFoodTimeEvent()
    {
        currentEvent = EventType.FoodTime;
        playerController.GoToCookingPos();
        keineController.GoToCookingPos();
        doneCounter = 0;
        keineController.BeginEvent(keineFoodTimeTargets);
        playerController.BeginEvent(mokouFoodTimeTargets);
        eventHasDiag = false;
    }

    /*
    public void DoPostFoodEvent()
    {
        player.SetActive(true);
        if (keineAlive)
            keine.SetActive(true);
        currentEvent = EventType.PostFood;
        playerController.GoToNightPos();
        keineController.GoToNightPos();
        doneCounter = 0;
        keineController.BeginEvent(keineAfterFoodTargets);
        playerController.BeginEvent(mokouAfterFoodTargets);
        eventHasDiag = true;
    }

    public void DoExitEvent()
    {
        Debug.Log("started exit event");
        currentEvent = EventType.Exit;
        doneCounter = 0;
        playerController.BeginEvent(null);
        keineController.BeginEvent(keineExitTarget);
        eventHasDiag = false;
    }
    */

    public void DoneMovement()
    {
        doneCounter++;
        if (doneCounter < 2)
            return;

        switch (currentEvent)
        {
            case EventType.Morning:
            //case EventType.PostFood:
                if (dialogController.StartDialogueSet(nextDiagId))
                {
                    keineController.FaceLeft();
                    playerController.FaceRight();
                    nextDiagId++;
                }
                break;
            case EventType.FoodTime:
                player.SetActive(false);
                mokouEating.SetActive(true);
                mokouEating.GetComponent<MokouEatingScript>().Initialize();
                if (keineAlive)
                    keine.SetActive(false);
                break;
            default:
                DoneEvent();
                break;
        }

        /*
        if (currentEvent == EventType.Morning || currentEvent == EventType.PostFood)
        {
            //start dialogue
            if (dialogController.StartDialogueSet(nextDiagId))
            {
                keineController.FaceLeft();
                playerController.FaceRight();
                nextDiagId++;
            }
        } else if (currentEvent == EventType.FoodTime)
        {
            //play anim, have an event at the end of that call the post food event
        } else
        {
            DoneEvent();
        }
        */
    }

    public void DoneDiag()
    {
        switch (currentEvent)
        {
            case EventType.Morning:
                DoneEvent();
                break;
            /*case EventType.PostFood:
                DoExitEvent();
                break;
                */
        }
    }

    public void DoneEvent()
    {
        playerController.canMoveFreely = true;
        keineController.canMoveFreely = true;
    }

    public void FinishEating()
    {
        //Debug.Log("call the thing here to have it fade out and go to next day");
        gameController.EndDay = true;
    }

    public void disableEatingAnims()
    {
        mokouEating.SetActive(false);
    }
}