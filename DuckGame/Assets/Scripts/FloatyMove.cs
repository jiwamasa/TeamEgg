using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyMove : MonoBehaviour 
{
    public float speed;
    public float lifeTimer = 10.0f;
    private float sinTimer = 0.0f;

    // Use this for initialization
    void Start() {}
	
	// Update is called once per frame
	void Update () 
    {
        lifeTimer -= Time.deltaTime;
        sinTimer += Time.deltaTime;

        transform.Translate(speed, 0.0f, 0.0f);

        if (lifeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
