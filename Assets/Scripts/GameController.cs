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
    private bool inScene = false;
    private int nextDiag = 0;
    private int morningTestCounter = 0; //ONLY FOR DEBUG, REMOVE LATER

    private const int NUM_TASKS = 4;

    public int Score
    {
        get => score; 
        set
        {
            score = value;
            if (score % NUM_TASKS == 0 && score > 0)
            {
                EndDay();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!inScene && Input.GetKeyDown(KeyCode.M) && (morningTestCounter < 2))
        {
            morningTestCounter++;
            StartMorningEvent(0);
        }

    }

    void EndDay()
    {
        uiController.FadeToBlack(true);
    }

    public void RestartLevel()
    {
        playerController.ResetLocation();
        dialogController.PlayNext();
        taskController.ResetAllTasks();
    }

    private void StartMorningEvent(int dayId)
    {
        inScene = true;
        keine.GetComponent<KeineController>().GoOffscreen();
        //player go to start
        player.GetComponent<PlayerController>().GoToHelloPosition();
        //keine start walking in
        keine.GetComponent<KeineController>().WalkToEvent();
    }

    public void StartDialogue()
    {
        keine.GetComponent<KeineController>().canFollow = false;
        player.GetComponent<PlayerController>().canMove = false;
        if (dialogController.StartDialogueSet(nextDiag))
            nextDiag++;
    }

    public void DoneDialogue()
    {
        keine.GetComponent<KeineController>().canFollow = true;
        player.GetComponent<PlayerController>().canMove = true;
        inScene = false;
    }
}
