using System.Threading;
using UnityEngine;


public class GameController : MonoBehaviour
{
    private int score; // number of tasks completed
    public TaskController taskController;
    public DialogueManager dialogController;
    public UIController uiController;
    public PlayerController playerController;
    private const int NUM_TASKS = 4;
    private bool endDay;

    public int Score
    {
        get => score; 
        set
        {
            score = value;
            if (score % NUM_TASKS == 0 && score > 0)
            {
                EndDay = true;
            }
        }
    }

    public bool EndDay { get => endDay; set => endDay = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLevel()
    {
        playerController.ResetLocation();
        dialogController.PlayNext();
        taskController.ResetAllTasks();
        EndDay = false;
    }
}
