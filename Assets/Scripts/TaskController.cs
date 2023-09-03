using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    private HashSet<ActivityController> tasks;
    public GameController gameController;

    public TaskController() { 
        tasks = new HashSet<ActivityController>();
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FinishTask(ActivityController controller)
    {
        Debug.Log("Finishing Task in TaskController");
        gameController.Score += 1;
    }

    public void ResetAllTasks()
    {
        foreach (var task in tasks.ToList())
        {
            task.ResetTask();
        }
    }

    public void AddActivityController(ActivityController activityController)
    {
        tasks.Add(activityController);
    }
}
