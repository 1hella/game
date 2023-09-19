using System.Threading;
using UnityEngine;


public class GameController : MonoBehaviour
{
    private int score; // number of tasks completed
    public TaskController taskController;
    public DialogueManager dialogController;
    public UIController uiController;
    public PlayerController playerController;
    public GameObject player;
    public GameObject keine;
    public KeineController keineController;
    public EventController eventController;
    private bool inScene = false;
    public bool debugEnabled;
    //private int nextDiag = 0;
    //private int morningTestCounter = 0; //ONLY FOR DEBUG, REMOVE LATER
    public int dayId = 1;
    private bool endingDay;
    private bool testingMorning;

    private const int NUM_TASKS = 4;
    private bool endDay;

    public int Score
    {
        get => score; 
        set
        {
            score = value;
            if (score % NUM_TASKS == 0 && score > 0 && !testingMorning)
            {
                eventController.DoFoodTimeEvent();
                endingDay = true;
            }
        }
    }

    public bool EndDay { get => endDay; set => endDay = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (!inScene)
        {
            //morningTestCounter++;
            eventController.DoMorningEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.B))
        {
            eventController.DoLategameMorning();
            //eventController.DoPostFoodEvent();
        }*/

        if (debugEnabled)
        {
            if (Input.GetKeyDown(KeyCode.N)) //finish the day immediately
            {
                Score += (NUM_TASKS - (score % NUM_TASKS));
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                morningTest(1);
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                morningTest(2);
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                morningTest(3);
            }
            else if (Input.GetKeyDown(KeyCode.F4))
            {
                morningTest(4);
            }
            else if (Input.GetKeyDown(KeyCode.F5))
            {
                morningTest(5);
            }
            else if (Input.GetKeyDown(KeyCode.F6))
            {
                morningTest(6);
            }
            else if (Input.GetKeyDown(KeyCode.F7))
            {
                morningTest(7);
            }
            else if (Input.GetKeyDown(KeyCode.F8))
            {
                morningTest(8);
            }
            else if (Input.GetKeyDown(KeyCode.F9))
            {
                morningTest(9);
            }
            else if (Input.GetKeyDown(KeyCode.F10))
            {
                morningTest(10);
            }
            else if (Input.GetKeyDown(KeyCode.F11))
            {
                morningTest(11);
            }
            else if (Input.GetKeyDown(KeyCode.F12))
            {
                morningTest(12);
            }
        }

        if (endingDay && Input.GetKeyDown(KeyCode.Space))
        {
            endDay = true;
            endingDay = false;
        }

    }

    public void RestartLevel()
    {
        testingMorning = false;
        taskController.ResetAllTasks();
        uiController.ResetAll();
        //playerController.ResetLocation();
        dayId++;
        eventController.disableEatingAnims();
        //dialogController.PlayNext();
        keine.SetActive(true);
        player.SetActive(true);
        //playerController.GoToMorningPos();
        //keineController.GoToMorningPos();
        EndDay = false;
        eventController.DoMorningEvent();
        
    }

    /**
     * 0 indexed
     */
    public int GetNumDay()
    {
        return score / NUM_TASKS;
    }

    private void morningTest(int dayNumber)
    {
        testingMorning = true;
        score = (dayNumber - 1) * 4;
        RestartLevel();
    }

    /*
    private void StartMorningEvent(int dayId)
    {
        inScene = true;
        keineController.GoToMorningPos();
        playerController.GoToMorningPos();
        keineController.WalkToEvent();
    }
    */

    /*public void StartDialogue()
    {
        keineController.canMoveFreely = false;
        playerController.canMoveFreely = false;
        if (dialogController.StartDialogueSet(nextDiag))
            nextDiag++;
    }
    */

    /*
    public void DoneDialogue()
    {
        keineController.canMoveFreely = true;
        playerController.canMoveFreely = true;
        inScene = false;
    }
    */
}
