using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private Vector2 rawPosition = Vector2.zero;
    public Vector2 startPosition;
    public GameObject thimble;
    private bool inWater = false;
    private bool hasReturned = true;
    private string state = "reel";
    private float bobTimer = 0.0f;
    private bool isVisible = false;

    bool splash = false;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = new Vector2(3f + thimble.transform.position.x, 1f + thimble.transform.position.y);
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
        hasReturned = true;
        isVisible = false;
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
            if (lastState != "cast")
            {
                thimble.GetComponent<ThimbleCastAnim>().AnimateCast();
            }
        }
        else
        {
            state = "reel";
        }

        if (state == "reel")
        {
            // smooth damp back to start position
            transform.position = Vector2.SmoothDamp(transform.position, startPosition, ref velocity, 0.075f);
            //remove velocity don't want it
            rb.velocity = Vector2.zero;

            // if at center then return to neutral
            float dist = Vector2.Distance(transform.position, startPosition);
            if (dist < 0.1f)
            {
                hasReturned = true;
                isVisible = false;

                // if (!thimble.GetComponent<ThimbleCastAnim>().GetCurrentAnimatorStateInfo(0).IsName("ThimblePull"))
                {
                    // thimble.GetComponent<ThimbleCastAnim>().AnimateIdle();
                }
            }

            inWater = false;
            splash = false;
        }

        if (state == "cast")
        {
            if (isVisible)
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
                    if (splash)
                    {
                        splash = true;
                        SFXPlayer.instance.PlaySFX("rod_splash");
                    }
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
                transform.position = Vector2.SmoothDamp(transform.position, startPosition, ref velocity, 0.075f);
            }
        }

        // GetComponent<SpriteRenderer>().enabled = isVisible;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<LineRenderer>().enabled = isVisible;
        // Debug.Log(isVisible);

        LineRenderer line = GetComponent<LineRenderer>();
        int lineCount = 10;
        Vector3[] positions = new Vector3[lineCount];
        for (int i=0; i<=lineCount-1; i++)
        {
            Vector2 temp = new Vector2((float)startPosition.x, (float)startPosition.y);
            Vector2 magnitude = new Vector2((float)(transform.position.x - startPosition.x), (float)(transform.position.y - startPosition.y));
            float scalar = ((float)i)/((float)(lineCount-1));
            float xdiff = (transform.position.x - startPosition.x)*scalar;
            float ydiff = (transform.position.y - startPosition.y)*scalar;
            temp.x += xdiff;
            temp.y += ydiff;

            temp.y -= Mathf.Pow((magnitude.magnitude/(float)lineCount), 2)*8.0f*(0.25f - Mathf.Abs((float)scalar-0.5f)*Mathf.Abs((float)scalar-0.5f));

            positions[i] = temp;
        }
        line.positionCount = lineCount;
        line.SetPositions(positions);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        FishBehavior fb = other.GetComponent<FishBehavior>();
        if (state == "reel" && fb)
        {
            thimble.GetComponent<ThimbleCastAnim>().AnimateReel();
            fb.FishOut();
        }
    }

    public void ThrowCast()
    {
        SFXPlayer.instance.PlaySFX("rod_cast2");
        rb.velocity = new Vector2(5.0f, 8.0f);
        isVisible = true;
    }
}
