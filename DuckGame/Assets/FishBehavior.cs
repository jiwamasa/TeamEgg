using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour {

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float dist = Vector2.Distance(this.transform.position, Vector2.zero);
		if (dist < 1f){
            rb.isKinematic = false;
            rb.AddForce(new Vector2 (0f, 3f), ForceMode2D.Impulse);
            //Camera.main.transform.parent = this.transform;
        }
	}
}
