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
    public bool lateGame = false;

    public DialogueManager dialogController;
    //private int nextDiagId = 0;
    private bool eventHasDiag = false;
    private int doneCounter = 0;
    private int dayCounter = 0;
    private int morningTestCounter = 0; //ONLY FOR DEBUG, REMOVE LATER

    enum EventType
    {
        Morning,
        FoodTime,
        LateMorning,
        LateFoodTime
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
        if (dayCounter > 5)
        {
            lateGame = true;
            DoLategameMorning();
                return;
        }
        currentEvent = EventType.Morning;
        playerController.GoToMorningPos();
        keineController.GoToMorningPos();
        doneCounter = 0;
        keineController.BeginEvent(keineMorningTarget);
        playerController.BeginEvent(null);
        eventHasDiag = true;
    }

    public void DoLategameMorning()
    {
        lateGame = true;
        if (dayCounter > 7)
        {
            keineAlive = false;
            mokouEating.GetComponent<MokouEatingScript>().isAlone = true;
        }
        currentEvent = EventType.LateMorning;
        playerController.GoToMorningPos();
        if (keineAlive)
        {
            keineController.GoToLateMorningPos();
            keineController.BeginEvent(null);
        } else
        {
            keine.SetActive(false);
        }
        doneCounter = 0;
        playerController.BeginEvent(null);
        eventHasDiag = true;
    }

    public void DoFoodTimeEvent()
    {
        if (dayCounter > 5)
        {
            DoLategameFoodTime();
            return;
        }
        currentEvent = EventType.FoodTime;
        playerController.GoToCookingPos();
        keineController.GoToCookingPos();
        doneCounter = 0;
        keineController.BeginEvent(keineFoodTimeTargets);
        playerController.BeginEvent(mokouFoodTimeTargets);
        eventHasDiag = false;
    }

    public void DoLategameFoodTime()
    {
        currentEvent = EventType.LateFoodTime;
        playerController.GoToCookingPos();
        //keineController.GoToCookingPos();
        if (keineAlive)
        {
            //keineController.GoToLateMorningPos();
            keineController.BeginEvent(null);
        }
        doneCounter = 0;
        keineController.BeginEvent(keineFoodTimeTargets);
        playerController.BeginEvent(mokouFoodTimeTargets);
        eventHasDiag = false;
    }

    public void DoneMovement()
    {
        doneCounter++;
        if (keineAlive && (doneCounter < 2))
            return;

        switch (currentEvent)
        {
            case EventType.Morning:
            case EventType.LateMorning:
                if (dialogController.StartDialogueSet(dayCounter))
                {
                    if (keineAlive)
                        keineController.FaceLeft();
                    playerController.FaceRight();
                    if (dayCounter < 12)
                        dayCounter++;
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

    }

    public void DoneDiag()
    {
        switch (currentEvent)
        {
            case EventType.Morning:
            case EventType.LateMorning:
                DoneEvent();
                break;
        }
    }

    public void DoneEvent()
    {
        playerController.canMoveFreely = true;
        if (!lateGame)
            keineController.canMoveFreely = true;
    }

    public void FinishEating()
    {
        gameController.EndDay = true;
    }

    public void disableEatingAnims()
    {
        mokouEating.SetActive(false);
    }
}