using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTetrisBlock : MonoBehaviour
{
    private float thrust = 2.5f;
    private float speed = 0.25f;
    private float rangeOfError = 0.1f;

    private int standStillThreshold = 50;
    private float standStillRange = 0.01f;
    private int standStillCounter = 0;
    private Vector2 standstillLastLocation;

    private float fallingIncrement = -0.05f;
    private float fastFallPercentIncrease = 1.0f;
    private float originalFallValue;

    private Rigidbody2D rb;

    private Vector2 targetPosition;
    private Vector2 previousPosition;
    private bool targetSet = false;

    private Vector3 startingRotation;
    private Vector3 targetRotation;
    private bool rotationSet = false;
    private float rotationDuration = 0.25f;
    private float counter = 0;

    public bool falling = true;
    private bool touchNext = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Input
        if (Input.GetKeyDown(KeyCode.A) && falling)
        {
            if (falling)
            {
                previousPosition = transform.position;
            }
            targetPosition = new Vector2(transform.position.x - thrust, transform.position.y);
            targetSet = true;
            falling = false;
        }
        if (Input.GetKeyDown(KeyCode.D) && falling)
        {
            if (falling)
            {
                previousPosition = transform.position;
            }
            targetPosition = new Vector2(transform.position.x + thrust, transform.position.y);
            targetSet = true;
            falling = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && falling)
        {
            startingRotation = transform.eulerAngles;
            targetRotation = new Vector3(startingRotation.x, startingRotation.y, startingRotation.z - 90);
            rotationSet = true;
            falling = false;
        }
        if (Input.GetKeyDown(KeyCode.Q) && falling)
        {
            startingRotation = transform.eulerAngles;
            targetRotation = new Vector3(startingRotation.x, startingRotation.y, startingRotation.z + 90);
            rotationSet = true;
            falling = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && falling)
        {
            originalFallValue = fallingIncrement;
            fallingIncrement += (fastFallPercentIncrease * originalFallValue);
        }
        if (Input.GetKeyUp(KeyCode.S) && falling)
        {
            fallingIncrement = originalFallValue;
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

        //Adjust rotation based on input
        if (rotationSet)
        {
            counter += Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(startingRotation, targetRotation, counter/ rotationDuration);

            if (counter >= rotationDuration)
            {
                rotationSet = false;
                falling = true;
                counter = 0;
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
            if (Vector2.Distance(standstillLastLocation, transform.position) < standStillRange) { 
                standStillCounter++;
                if(standStillCounter >= standStillThreshold)
                {
                    //TK Lock in the object, make it child of duck and remove rigidbody
                    transform.parent = GameObject.Find("Duck").transform;
                    //rb.isKinematic = true;
                }
            }
            else
            {
                standStillCounter = 0;
                standstillLastLocation = (Vector2)transform.position;
            }
        }

        //Check for touchNext
        if(touchNext && !falling)
        {
            touchNext = false;
            touchingDuck();
        }
    }

    //We have touched the duck
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Duck" || collision.tag == "TetrisBlock")
        {
            if (falling)
            {
                touchingDuck();
            }
            else
            {
                touchNext = true;
            }
        }
        else if(collision.tag == "Bounding")
        {
            if (targetSet)
            {
                targetPosition = previousPosition;
            }
        }
    }

    private void touchingDuck()
    {

        //Become Dynamic, cease falling
        falling = false;
        rb.isKinematic = false;
        standstillLastLocation = (Vector2)transform.position;

        targetSet = false;
        rotationSet = false;
    }
}
