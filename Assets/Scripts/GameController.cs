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
}
