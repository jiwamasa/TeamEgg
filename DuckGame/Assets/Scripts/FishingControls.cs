using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private string state = "neutral";
    private float bobTimer = 0.0f;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(state);
        bobTimer += Time.deltaTime;

        string lastState = state;
        state = Input.GetKey("space") ? "cast" : "reel";

        if (state == "reel")
        {
            // smooth damp back to origin
            transform.position = Vector2.SmoothDamp(transform.position, Vector2.zero, ref velocity, 0.1f);
            //remove velocity don't want it
            rb.velocity = Vector2.zero;

            // if at center then return to neutral
            float dist = Vector2.Distance(transform.position, Vector2.zero);
            if (dist < 0.1f)
            {
                state = "neutral";
            }
        }

        // when hook is first cast, give it sidways velocity
        if (state == "cast" && lastState != "cast")
        {
            rb.velocity = new Vector2(4.0f, 2.0f);
        }

        if (state == "cast")
        {
            if (transform.position.y < Constants.WaterLevel)
            {
                // smooth damp back to top of water
                // floating effect
                Vector2 towards = new Vector2(transform.position.x, Constants.WaterLevel);
                transform.position = Vector2.SmoothDamp(transform.position, towards, ref velocity, 0.1f);
                //remove velocity don't want it
                rb.velocity = Vector2.zero;
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
