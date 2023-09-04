using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerBody;
    public float speed;
    public bool canMove = true;
    private Vector3 initialPosition;
    private Animator animator;
    //public Vector3 helloPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        initialPosition = playerBody.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (movement != Vector2.zero)
            {
                animator.SetFloat("XInput", movement.x);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
            
            playerBody.velocity = movement * speed;
        } else
        {
            playerBody.velocity = new Vector2(0, 0);
        }
    }

    public void ResetLocation() { 
        playerBody.position = initialPosition;
    }

    public void GoToHelloPosition()
    {
        canMove = false;
        playerBody.position = initialPosition;
    }
}
