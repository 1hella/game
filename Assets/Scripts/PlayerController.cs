using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterControllerScript
{
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = body.position;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    protected override void DefaultMovement()
    {
        if (canMoveFreely)
        {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            body.velocity = movement * speed;
        }
        else
            body.velocity = new Vector2(0, 0);

    }

    public void ResetLocation() {
        body.position = initialPosition;
    }

    /*public void GoToHelloPosition()
    {
        canMoveFreely = false;
        body.position = initialPosition;
    }
    */
}
