using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyMove : MonoBehaviour
{
    public float speed;
    public float lifeTimer = 30.0f;
    private float sinTimer = 0.0f;
    public bool isMoving = true;

    private Vector2 startingPosition;
    private Vector2 targetPosition;

    // Use this for initialization
    void Start() {
        startingPosition = transform.position;
        targetPosition = new Vector2(transform.position.x - 30f, transform.position.y);
    }

	// Update is called once per frame
	void Update ()
    {
       
        if (isMoving)
        {
            sinTimer += Time.deltaTime;

            transform.position = Vector2.Lerp(startingPosition, targetPosition, sinTimer / lifeTimer);
        }

        if (sinTimer >= lifeTimer * 2)
        {
            Destroy(this.gameObject);
        }
	}
}
