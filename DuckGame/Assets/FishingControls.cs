using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingControls : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private string state = "reel";

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        state = Input.GetKey("space") ? "cast" : "reel";

        if (state == "reel") {
            transform.position = Vector2.SmoothDamp(transform.position, Vector2.zero, ref velocity, 0.1f);
            rb.velocity = Vector2.zero;
            float dist = Vector2.Distance(transform.position, Vector2.zero);
        } else if (state == "cast") {

        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (state == "reel" && other.GetComponent<FishBehavior>())
        {
            //share position
            other.transform.parent = this.transform;

            //share velocity
            //FixedJoint2D joint = other.gameObject.GetComponent<FixedJoint2D>();
            //joint.connectedBody = rb;
            //joint.enabled = true;
        }
    }

}
