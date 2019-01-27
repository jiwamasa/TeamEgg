using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private Vector2 rawPosition = Vector2.zero;
    public Vector2 startPosition;
    private bool inWater = false;
    private bool hasReturned = false;
    private string state = "neutral";
    private float bobTimer = 0.0f;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = new Vector2(3, 0);
        transform.position = startPosition;
	}

	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(state);
        bobTimer += Time.deltaTime;

        string lastState = state;

        if (Input.GetKey("space") && (hasReturned || state == "cast"))
        {
            state = "cast";
        }
        else
        {
            state = "reel";
        }

        if (state == "reel")
        {
            // smooth damp back to start position
            transform.position = Vector2.SmoothDamp(transform.position, startPosition, ref velocity, 0.1f);
            //remove velocity don't want it
            rb.velocity = Vector2.zero;

            // if at center then return to neutral
            float dist = Vector2.Distance(transform.position, startPosition);
            if (dist < 0.1f)
            {
                state = "neutral";
                hasReturned = true;
            }

            inWater = false;
        }

        // when hook is first cast, give it sidways velocity
        if (state == "cast" && lastState != "cast")
        {
            rb.velocity = new Vector2(5.0f, 5.0f);
        }

        if (state == "cast")
        {
            hasReturned = false;

            if (transform.position.y < Constants.WaterLevel)
            {
                rb.gravityScale = 0.5f;
                inWater = true;
            }
            else
            {
                rb.gravityScale = 2.5f;
            }

            if (inWater)
            {
                // smooth damp back to top of water
                // floating effect
                Vector2 towards = new Vector2(transform.position.x, Constants.WaterLevel);
                rawPosition = Vector2.SmoothDamp(transform.position, towards, ref velocity, 1.0f);
                //remove velocity don't want it
                rb.velocity = Vector2.zero;

                transform.position = rawPosition;
                // transform.position.y += Mathf.Sin(bobTimer);
            }
        }

        LineRenderer line = GetComponent<LineRenderer>();
        int lineCount = 5;
        Vector3[] positions = new Vector3[lineCount];
        for (int i=0; i<=lineCount-1; i++)
        {
            Vector2 temp = new Vector2((float)startPosition.x, (float)startPosition.y);
            float scalar = ((float)i)/((float)(lineCount-1));
            float xdiff = (transform.position.x - startPosition.x)*scalar;
            float ydiff = (transform.position.y - startPosition.y)*scalar;
            temp.x += xdiff;
            temp.y += ydiff;

            // // Vector2 angleRef = new Vector2(transform.position.x, startPosition.y)
            float angle = GetAngle(transform.position.x, startPosition.y, temp.x,temp.y);
            // float angle = Mathf.Atan2(temp.y-startPosition.y, temp.x-transform.position.x);

            temp.x += Mathf.Cos(angle);
            temp.y += Mathf.Sin(angle);

            positions[i] = temp;
            Debug.Log("" + transform.position.x + ", " + startPosition.x + " xdiff = " + xdiff);
            // positions[i] = startPosition + (transform.position*(i/lineCount));
        }
        //positions[lineCount-1] = transform.position;
        line.positionCount = lineCount;
        line.SetPositions(positions);
    }

    public static float GetAngle(float x1, float y1, float x2, float y2)
    {
        return Mathf.Atan2(y2-y1, x2-x1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        FishBehavior fb = other.GetComponent<FishBehavior>();
        if (state == "reel" && fb)
        {
            fb.FishOut();
        }
    }
}
