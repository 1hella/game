using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{

    public PlayerController playerController;
    public KeineController keineController;
    public GameController gameController;

    public DialogueManager dialogController;
    private int nextDiagId = 0;
    private bool eventHasDiag = false;
    private bool isFoodEvent = false;
    private int doneCounter = 0;
    private int morningTestCounter = 0; //ONLY FOR DEBUG, REMOVE LATER

    private Vector2[] keineMorningTarget = {
        new Vector2(0, -1.9f) };

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
        playerController.GoToMorningPos();
        keineController.GoToMorningPos();
        keineController.BeginEvent(keineMorningTarget);
        playerController.BeginEvent(null);
        eventHasDiag = true;
    }

    public void DoneMovement()
    {
        
        if (eventHasDiag)
        {
            //start dialogue
            if (dialogController.StartDialogueSet(nextDiagId))
                nextDiagId++;
        }
        else if (isFoodEvent)
        {
            //do that
        } else
        {
            DoneEvent();
        }
    }

    public void DoneEvent()
    {
        playerController.canMoveFreely = true;
        keineController.canMoveFreely = true;
    }
}