using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityController : MonoBehaviour
{
    public GameObject player;
    public BoxCollider2D activityCollider;
    public Animator animator;
    private bool startedTask;
    private bool doneTask;
    private bool inActivationRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (startedTask && Input.GetKeyDown(KeyCode.Space))
        {
            FinishTask();
        }

        if (!startedTask && inActivationRange && Input.GetKeyDown(KeyCode.Space))
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

    private void BeginTask()
    {
        Debug.Log("started task");
        startedTask = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        player.GetComponent<PlayerController>().canMove = false;
    }

    private void FinishTask()
    {
        Debug.Log("finished task");
        doneTask = true;
        animator.SetBool("Done", true);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        player.GetComponent<PlayerController>().canMove = true;
    }
}
