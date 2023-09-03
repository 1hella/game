using UnityEngine;

public class ActivityController : MonoBehaviour
{
    public GameObject player;
    public GameObject keine;
    public GameObject taskPlayer;
    public BoxCollider2D activityCollider;
    public Animator animator;
    public TaskController taskController;
    public UIController uiController;
    public Vector2 newPlayerPosition;
    public Vector2 newKeinePosition;
    public TaskScript taskScript;
    
    private bool startedTask;
    private bool doneTask;
    private bool inActivationRange;
    private int count;


    // Start is called before the first frame update
    void Start()
    {
        taskController.AddActivityController(this);
    }

    // Update is called once per frame
    void Update()
    {

        if (startedTask && Input.GetKeyDown(KeyCode.Space))
        {
            bool finished = taskScript.progress();
            if (finished)
            {
                FinishTask();
            }
        }

        if (!uiController.isFadedToBlack() && !startedTask && !doneTask && inActivationRange && Input.GetKeyDown(KeyCode.Space))
        {
            BeginTask();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player entered this object's hitbox");
            inActivationRange = true;
            animator.SetBool("DoableNow", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player is now outside this object's hitbox");
            inActivationRange = false;
            animator.SetBool("DoableNow", false);
        }
    }

    public void ResetTask()
    {
        Debug.Log("reset task");
        startedTask = false;
        animator.SetBool("Done", false);
        doneTask = false;
    }

    private void BeginTask()
    {
        Debug.Log("started task");
        startedTask = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        player.SetActive(false);
        player.GetComponent<PlayerController>().canMove = false;
        keine.SetActive(false);
        keine.GetComponent<KeineController>().canFollow = false;
        snapCharacterPositions();
        //player.transform.position = new Vector3(-6,-3,-1);
        taskScript.startTask();
    }

    private void FinishTask()
    {
        Debug.Log("finished task");
        //doneTask = true;
        animator.SetBool("Done", true);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        player.SetActive(true);
        player.GetComponent<PlayerController>().canMove = true;
        keine.SetActive(true);
        keine.GetComponent<KeineController>().canFollow = true;
        taskController.FinishTask(this);
        startedTask = false;
        doneTask = true;
        taskScript.stopTask();
    }

    private void snapCharacterPositions ()
    {
        player.transform.position = new Vector3(newPlayerPosition.x, newPlayerPosition.y, player.transform.position.z);
        keine.transform.position = new Vector3(newKeinePosition.x, newKeinePosition.y, keine.transform.position.z);
    }
}
