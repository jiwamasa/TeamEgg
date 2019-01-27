using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private Vector2 rawPosition = Vector2.zero;
    public Vector2 startPosition = Vector2.zero;//new Vector2(10.0f, 0.0f);
    private bool inWater = false;
    private bool hasReturned = false;
    private string state = "neutral";
    private float bobTimer = 0.0f;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = new Vector2(4.0f, 2.0f);
        }

        if (state == "cast")
        {
            hasReturned = false;

            if (transform.position.y < Constants.WaterLevel)
            {
                inWater = true;
            }

            if (inWater)
            {
                // smooth damp back to top of water
                // floating effect
                Vector2 towards = new Vector2(transform.position.x, Constants.WaterLevel);
                rawPosition = Vector2.SmoothDamp(transform.position, towards, ref velocity, 0.1f);
                //remove velocity don't want it
                rb.velocity = Vector2.zero;

                transform.position = rawPosition;
                // transform.position.y += Mathf.Sin(bobTimer);
            }
        }
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
