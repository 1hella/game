using UnityEngine;

public class PlayerController : CharacterControllerScript
{

    private Rigidbody2D playerBody;
    public bool canMove = true;
    private Animator animator;
    public Transform followerTransform;
    //public Vector3 helloPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        initialPosition = body.position;
        SetAnimator(animator);
    }

    protected override void DefaultMovement()
    {
        if (canMoveFreely)
        {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            body.velocity = movement * speed;
            if (movement != Vector2.zero)
            {
                animator.SetFloat("XInput", movement.x);
                animator.SetBool("isWalking", true);
                if (transform.position.y < followerTransform.position.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, followerTransform.position.z - 0.1f);
                } else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, followerTransform.position.z + 0.1f);
                }
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
            body.velocity = new Vector2(0, 0);
    }

    public void ResetLocation() {
        body.position = initialPosition;
    }

    public void FaceRight()
    {
        animator.SetFloat("XInput", 1);
        animator.SetBool("isWalking", false);
    }

    /*public void GoToHelloPosition()
    {
        canMoveFreely = false;
        body.position = initialPosition;
    }
    */
}
