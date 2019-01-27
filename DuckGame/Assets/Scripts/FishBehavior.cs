using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour {

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    public void FishOut () {
        Debug.Log("Fished Out");
        rb.isKinematic = false;
        rb.AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
        GetComponent<FloatyMove>().isMoving = false;
        //Camera.main.transform.parent = this.transform;
        Camera.main.GetComponent<CameraMove>().MoveTo(new Vector2 (0f,2f));
    }
}
