using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeineController : MonoBehaviour
{
    public GameObject player;
    public Transform keineTransform;
    public float followDistance;
    public float speed;
    public Rigidbody2D body;

    public bool canMove = true;
    private bool sitting = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(keineTransform.position, player.transform.position);
        if (canMove && !sitting && (dist > followDistance))
        {
            Vector2 playerPos = player.transform.position;
            Vector2 keinePos = keineTransform.position;
            body.velocity = (playerPos - keinePos).normalized * speed;
        }
        else
        {
            body.velocity = new Vector3(0,0,0);
        }
    }
}
