using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTetrisBlock : MonoBehaviour
{
    public float thrust = 0.5f;
    public float speed = 0.1f;
    public float rangeOfError = 0.05f;

    public int standStillThreshold = 200;
    private int standStillCounter = 0;
    private Vector2 standstillLastLocation;

    public float fallingIncrement = 0.1f;
    private Rigidbody2D rb;

    private Vector2 targetPosition;
    private bool targetSet = false;

    public bool falling = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && falling)
        {
            targetPosition = new Vector2(transform.position.x - thrust, transform.position.y);
            targetSet = true;
            falling = false;
        }
        if (Input.GetKeyDown(KeyCode.D) && falling)
        {
            targetPosition = new Vector2(transform.position.x + thrust, transform.position.y);
            targetSet = true;
            falling = false;
        }

        //Adjust position based on input
        if (targetSet)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, speed);

            if(transform.position.x <= targetPosition.x + rangeOfError && transform.position.x >= targetPosition.x - rangeOfError)
            {
                targetSet = false;
                falling = true;
            }
        }

        //Constant Falling
        if (falling)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + fallingIncrement);
        }

        //Check for standstill after leaving control
        if(!falling && !rb.isKinematic)
        {
            if (standstillLastLocation == (Vector2)transform.position)
            {
                standStillCounter++;
                if(standStillCounter >= standStillThreshold)
                {
                    //TK Lock in the object
                }
            }
            else
            {
                standStillCounter = 0;
                standstillLastLocation = (Vector2)transform.position;
            }
        }
    }

    //We have touched the duck
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Become Dynamic, cease falling
        falling = false;
        rb.isKinematic = false;
        standstillLastLocation = (Vector2)transform.position;
    }
}
